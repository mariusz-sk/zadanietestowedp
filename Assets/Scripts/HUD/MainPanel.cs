using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AgentSimulator.UI
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField]
        private DataProvider _dataProvider;

        [SerializeField]
        private TextMeshProUGUI _spawnedAgentsCountText;

        [SerializeField]
        private TextMeshProUGUI _selectedAgentNameText;

        [SerializeField]
        private TextMeshProUGUI _selectedAgentHealthText;

        private void OnEnable()
        {
            if (_dataProvider == null)
                return;

            _dataProvider.DataChangedEvent += OnDataProviderDataChanged;
        }

        private void OnDisable()
        {
            if (_dataProvider == null)
                return;

            _dataProvider.DataChangedEvent -= OnDataProviderDataChanged;
        }

        private void OnDataProviderDataChanged()
        {
            if (_spawnedAgentsCountText != null)
            {
                var text = string.Format($"Agent count: {_dataProvider.SpawnedAgentsNumber}/{_dataProvider.MaxAllowedAgentsNumber}");
                _spawnedAgentsCountText.text = text;
            }
        }
    }
}
