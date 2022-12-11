using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.UI
{
    public class DataProvider : MonoBehaviour
    {
        [SerializeField]
        private AgentSpawner _agentSpawner;

        public delegate void DataProviderEventHandler();
        public event DataProviderEventHandler DataChangedEvent;

        public int MaxAllowedAgentsNumber
        {
            get
            { 
                return _agentSpawner != null ? _agentSpawner.MaxAllowedAgentsNumber : 0;
            }
        }

        public int SpawnedAgentsNumber
        {
            get
            {
                return _agentSpawner != null ? _agentSpawner.NumSpawnedAgents : 0;
            }
        }

        private void OnEnable()
        {
            if (_agentSpawner == null)
                return;

            _agentSpawner.AgentSpawnedEvent += OnAgentSpawned;
            _agentSpawner.AgentKilledEvent += OnAgentKilled;
        }

        private void OnDisable()
        {
            if (_agentSpawner == null)
                return;

            _agentSpawner.AgentSpawnedEvent -= OnAgentSpawned;
            _agentSpawner.AgentKilledEvent -= OnAgentKilled;
        }

        private void OnAgentSpawned()
        {
            DataChangedEvent?.Invoke();
        }
        
        private void OnAgentKilled()
        {
            DataChangedEvent?.Invoke();
        }
    }
}
