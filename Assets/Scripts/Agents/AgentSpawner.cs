using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator
{
    public class AgentSpawner : MonoBehaviour
    {
        [SerializeField]
        private Agent[] _agentPrefabs;

        [SerializeField, Range(2.0f, 10.0f)]
        private float _spawnAgentDelay = 5.0f;

        [SerializeField, Range(1, 60)]
        private int _maxAllowedAgentNumber = 30;

        private Transform _transform;

        private Dictionary<Agent, List<Agent>> _agentLists;

        private int _numSpawnedAgents = 0;

        private float _nextSpawnAgentTime = 0;

        private void Awake()
        {
            if (_agentPrefabs == null || _agentPrefabs.Length < 1)
            {
                enabled = false;
                return;
            }

            _transform = GetComponent<Transform>();

            _agentLists = new Dictionary<Agent, List<Agent>>(_agentPrefabs.Length);

            for (int i = 0; i < _agentPrefabs.Length; i++)
            {
                List<Agent> agentList = CreateAgentListForPrefab(_agentPrefabs[i]);

                _agentLists.Add(_agentPrefabs[i], agentList);
            }

            _nextSpawnAgentTime = Time.timeSinceLevelLoad + _spawnAgentDelay;
        }

        private void OnEnable()
        {
            foreach (var agentList in _agentLists.Values)
            {
                foreach (var agent in agentList)
                {
                    if (agent != null)
                    {
                        agent.OnAgentDeactivatedEvent += OnAgentDeactivatedEvent;
                    }
                }
            }
        }

        private void OnDisable()
        {
            foreach (var agentList in _agentLists.Values)
            {
                foreach (var agent in agentList)
                {
                    if (agent != null)
                    {
                        agent.OnAgentDeactivatedEvent -= OnAgentDeactivatedEvent;
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

            SpawnNewAgent(_transform.position);

            _nextSpawnAgentTime = Time.timeSinceLevelLoad + _spawnAgentDelay;
        }

        private List<Agent> CreateAgentListForPrefab(Agent agentPrefab)
        {
            var agentList = new List<Agent>(_maxAllowedAgentNumber);
            for (int j = 0; j < _maxAllowedAgentNumber; j++)
            {
                Agent agent = GameObject.Instantiate<Agent>(agentPrefab, Vector3.zero, Quaternion.identity, _transform);
                agent.gameObject.SetActive(false);

                agentList.Add(agent);
            }

            return agentList;
        }

        private Agent SpawnNewAgent(Vector3 position)
        {
            int prefabIndex = Random.Range(0, _agentPrefabs.Length);

            Agent newAgent = FindFreeAgentForPrefab(_agentPrefabs[prefabIndex]);

            if (newAgent != null)
            {
                newAgent.Spawn(position);
                
                _numSpawnedAgents++;
            }

            return newAgent;
        }

        private Agent FindFreeAgentForPrefab(Agent agentPrefab)
        {
            Agent spawnedAgent = null;

            List<Agent> agentList;
            if (_agentLists.TryGetValue(agentPrefab, out agentList))
            {
                foreach (var agent in agentList)
                {
                    if (agent != null && !agent.gameObject.activeInHierarchy)
                    {
                        spawnedAgent = agent;

                        break;
                    }
                }
            }

            return spawnedAgent;
        }

        private void OnAgentDeactivatedEvent(Agent agent)
        {
            foreach (var agentList in _agentLists.Values)
            {
                if (agentList.Contains(agent))
                {
                    _numSpawnedAgents--;

                    break;
                }
            }

            
        }
    }
}
