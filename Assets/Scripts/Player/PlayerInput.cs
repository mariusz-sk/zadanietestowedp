using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private SelectionManager _selectionManager;


        private void Update()
        {
            HandleMouseInput();
        }

        private void HandleMouseInput()
        {
            if (_camera == null || _selectionManager == null)
                return;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _selectionManager.TrySelectEntityInScene(_camera.ScreenPointToRay(Input.mousePosition));
            }
        }
    }
}
