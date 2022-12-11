using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.Agents
{
    // AgentSpawner is a simple implementation of object pool.
    // Allows to assign many different prefabs and creates one object pool for every assigned prefab.

    public class AgentSpawner : MonoBehaviour
    {
        [SerializeField]
        private Agent[] _agentPrefabs;

        [SerializeField, Range(2.0f, 10.0f)]
        private float _spawnAgentDelay = 5.0f;

        [SerializeField, Range(1, 60)]
        private int _maxAllowedAgentNumber = 30;


        public int MaxAllowedAgentsNumber { get => _maxAllowedAgentNumber; }
        public int NumSpawnedAgents { get => _numSpawnedAgents; }


        public delegate void AgentSpawnerEventHandler();
        public event AgentSpawnerEventHandler AgentSpawnedEvent;
        public event AgentSpawnerEventHandler AgentKilledEvent;


        private Transform _transform;

        private Dictionary<Agent, List<Agent>> _agentLists;

        private int _numSpawnedAgents = 0;

        private float _nextSpawnAgentTime = 0;

        private int _nextAgentNumber = 0;

        private void Awake()
        {
            // Disable this component if there are no prefabs assigned in the Inspector
            if (_agentPrefabs == null || _agentPrefabs.Length < 1)
            {
                enabled = false;
                return;
            }

            _transform = GetComponent<Transform>();

            // Create dictionary for object pools
            _agentLists = new Dictionary<Agent, List<Agent>>(_agentPrefabs.Length);

            // Create object pool for every assigned prefab
            for (int i = 0; i < _agentPrefabs.Length; i++)
            {
                List<Agent> agentList = CreateAgentListForPrefab(_agentPrefabs[i]);

                _agentLists.Add(_agentPrefabs[i], agentList);
            }

            _nextSpawnAgentTime = Time.timeSinceLevelLoad + _spawnAgentDelay;
        }

        private void OnEnable()
        {
            // Register for events from every instantiated agent
            foreach (var agentList in _agentLists.Values)
            {
                foreach (var agent in agentList)
                {
                    if (agent != null)
                    {
                        agent.OnKilledEvent += OnAgentKilledEvent;
                    }
                }
            }
        }

        private void OnDisable()
        {
            // Unregister from events from every instatntiated agent 
            foreach (var agentList in _agentLists.Values)
            {
                foreach (var agent in agentList)
                {
                    if (agent != null)
                    {
                        agent.OnKilledEvent -= OnAgentKilledEvent;
                    }
                }
            }
        }

        private void Update()
        {
            if (_numSpawnedAgents >= _maxAllowedAgentNumber)
                return;

            if (Time.timeSinceLevelLoad < _nextSpawnAgentTime)
                return;

            SpawnNewAgent();

            _nextSpawnAgentTime = Time.timeSinceLevelLoad + _spawnAgentDelay;
        }

        private List<Agent> CreateAgentListForPrefab(Agent agentPrefab)
        {
            // We don't know how many agents will be (randomly) spawned for each assigned prefab.
            // So we instantiate maximum allowed agent number objects for every prefab, in case that the entire allowable number of agents comes from only one prefab.
            var agentList = new List<Agent>(_maxAllowedAgentNumber);
            for (int j = 0; j < _maxAllowedAgentNumber; j++)
            {
                Agent agent = GameObject.Instantiate<Agent>(agentPrefab, Vector3.zero, Quaternion.identity, _transform);
                agent.gameObject.SetActive(false);

                agentList.Add(agent);
            }

            return agentList;
        }

        private Agent SpawnNewAgent()
        {
            // Choose random prefab
            int prefabIndex = Random.Range(0, _agentPrefabs.Length);

            // Try to find one free (deactivated) agent object in pool
            Agent newAgent = FindFreeAgentForPrefab(_agentPrefabs[prefabIndex]);

            if (newAgent != null)
            {
                // Generate proper name for this new agent (based on prefab name and next "free" number)
                _nextAgentNumber++;
                string agentName = string.Format("{0}_{1,0:D4}", _agentPrefabs[prefabIndex].name, _nextAgentNumber);

                // Spawn new agent at position of AgentSpawner
                newAgent.Spawn(_transform.position, agentName);
                _numSpawnedAgents++;

                AgentSpawnedEvent?.Invoke();
            }

            return newAgent;
        }

        private Agent FindFreeAgentForPrefab(Agent agentPrefab)
        {
            Agent spawnedAgent = null;

            // Get object pool for given agent prefab
            if (_agentLists.TryGetValue(agentPrefab, out List<Agent> agentList))
            {
                // Check every agent on object pool
                foreach (var agent in agentList)
                {
                    // If there is not null object and is not active then we've got object that we can reuse
                    if (agent != null && !agent.gameObject.activeInHierarchy)
                    {
                        spawnedAgent = agent;

                        break;
                    }
                }
            }

            return spawnedAgent;
        }

        private void OnAgentKilledEvent(Agent agent)
        {
            // Check if given agent is on any our object pool
            foreach (var agentList in _agentLists.Values)
            {
                if (agentList.Contains(agent))
                {
                    _numSpawnedAgents--;

                    AgentKilledEvent?.Invoke();

                    break;
                }
            }
        }
    }
}
