using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.Player
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _layerMask;


        public bool IsAnyEntitySelected { get => _selectedEntity != null; }
        public SelectableEntity SelectedEntity { get => _selectedEntity; }

        public delegate void SelectionManagerHandler();
        public event SelectionManagerHandler EntitySelectedEvent;
        public event SelectionManagerHandler EntityDeselectedEvent;


        private SelectableEntity _selectedEntity;


        public bool TrySelectEntityInScene(Ray ray)
        {
            DeselectSelected();

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100.0f, _layerMask))
            {
                var selectableEntity = hitInfo.collider.GetComponentInParent<SelectableEntity>();
                if (selectableEntity != null)
                {
                    SelectEntity(selectableEntity);
                }
            }

            return _selectedEntity != null;
        }

        private void OnEntityDeselected(SelectableEntity selectableEntity)
        {
            if (selectableEntity != null && selectableEntity == _selectedEntity)
            {
                DeselectSelected();
            }
        }

        private void SelectEntity(SelectableEntity selectableEntity)
        {
            _selectedEntity = selectableEntity;

            if (_selectedEntity != null)
            {
                _selectedEntity.EntityDeselectedEvent += OnEntityDeselected;

                _selectedEntity.OnSelected();
                EntitySelectedEvent?.Invoke();
            }
        }

        private void DeselectSelected()
        {
            if (_selectedEntity != null)
            {
                _selectedEntity.OnDeselected();
                _selectedEntity.EntityDeselectedEvent -= OnEntityDeselected;
            }

            _selectedEntity = null;
            EntityDeselectedEvent?.Invoke();
        }
    }
}
