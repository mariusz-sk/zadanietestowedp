using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.UI
{
    public class DataProvider : MonoBehaviour
    {
        [SerializeField]
        private Agents.AgentSpawner _agentSpawner;

        [SerializeField]
        private Player.SelectionManager _selectionManager;

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

        public bool IsAnyAgentSelected
        {
            get
            {
                return _selectionManager != null ? _selectionManager.IsAnyAgentSelected : false;
            }
        }

        public string SelectedAgentName
        {
            get
            {
                if (_selectionManager != null && _selectionManager.SelectedAgent != null)
                    return _selectionManager.SelectedAgent.name;
                else
                    return "";
            }
        }

        public int SelectedAgentHealthValue
        {
            get
            {
                if (_selectionManager != null && _selectionManager.SelectedAgent != null)
                {
                    if (_selectionManager.SelectedAgent.TryGetComponent<Agents.Health>(out Agents.Health health))
                    {
                        return health.CurrentValue;
                    }
                }

                return 0;
            }
        }

        private void OnEnable()
        {
            if (_agentSpawner != null)
            {
                _agentSpawner.AgentSpawnedEvent += OnAgentSpawned;
                _agentSpawner.AgentKilledEvent += OnAgentKilled;
            }

            if (_selectionManager != null)
            {
                _selectionManager.AgentSelectedEvent += OnAgentSelected;
                _selectionManager.AgentDeselectedEvent += OnAgentDeselected;
            }
        }

        private void OnDisable()
        {
            if (_agentSpawner != null)
            {
                _agentSpawner.AgentSpawnedEvent -= OnAgentSpawned;
                _agentSpawner.AgentKilledEvent -= OnAgentKilled;
            }

            if (_selectionManager != null)
            {
                _selectionManager.AgentSelectedEvent -= OnAgentSelected;
                _selectionManager.AgentDeselectedEvent -= OnAgentDeselected;
            }
        }

        private void OnAgentSpawned()
        {
            DataChangedEvent?.Invoke();
        }
        
        private void OnAgentKilled()
        {
            DataChangedEvent?.Invoke();
        }

        private void OnAgentSelected()
        {
            DataChangedEvent?.Invoke();
        }

        private void OnAgentDeselected()
        {
            DataChangedEvent?.Invoke();
        }
    }
}
