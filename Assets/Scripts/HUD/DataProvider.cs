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
                return _selectionManager != null ? _selectionManager.IsAnyEntitySelected : false;
            }
        }

        public string SelectedAgentName
        {
            get
            {
                if (_selectionManager != null && _selectionManager.SelectedEntity != null)
                    return _selectionManager.SelectedEntity.name;
                else
                    return "";
            }
        }

        public int SelectedAgentHealthValue
        {
            get
            {
                if (_selectionManager != null && _selectionManager.SelectedEntity != null)
                {
                    if (_selectionManager.SelectedEntity.TryGetComponent<Agents.Health>(out Agents.Health health))
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
                _selectionManager.EntitySelectedEvent += OnEntitySelected;
                _selectionManager.EntityDeselectedEvent += OnEntityDeselected;
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
                _selectionManager.EntitySelectedEvent -= OnEntitySelected;
                _selectionManager.EntityDeselectedEvent -= OnEntityDeselected;
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

        private void OnEntitySelected()
        {
            DataChangedEvent?.Invoke();
        }

        private void OnEntityDeselected()
        {
            DataChangedEvent?.Invoke();
        }
    }
}
