using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.Player
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _layerMask;


        public bool IsAnyAgentSelected { get => _selectedAgent != null; }
        public SelectableAgent SelectedAgent { get => _selectedAgent; }

        public delegate void SelectionManagerHandler();
        public event SelectionManagerHandler AgentSelectedEvent;
        public event SelectionManagerHandler AgentDeselectedEvent;


        private SelectableAgent _selectedAgent;


        public bool TrySelectAgentInScene(Ray ray)
        {
            DeselectSelected();

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100.0f, _layerMask))
            {
                var selectableAgent = hitInfo.collider.GetComponentInParent<SelectableAgent>();
                if (selectableAgent != null)
                {
                    SelectAgent(selectableAgent);
                }
            }

            return _selectedAgent != null;
        }

        private void OnAgentDeselected(SelectableAgent selectableAgent)
        {
            if (selectableAgent != null && selectableAgent == _selectedAgent)
            {
                DeselectSelected();
            }
        }

        private void SelectAgent(SelectableAgent selectableAgent)
        {
            _selectedAgent = selectableAgent;

            if (_selectedAgent != null)
            {
                _selectedAgent.AgentDeselectedEvent += OnAgentDeselected;

                _selectedAgent.OnSelected();
                AgentSelectedEvent?.Invoke();
            }
        }

        private void DeselectSelected()
        {
            if (_selectedAgent != null)
            {
                _selectedAgent.OnDeselected();
                _selectedAgent.AgentDeselectedEvent -= OnAgentDeselected;
            }

            _selectedAgent = null;
            AgentDeselectedEvent?.Invoke();
        }
    }
}
