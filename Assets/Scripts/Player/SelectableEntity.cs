using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.Player
{
    public class SelectableEntity : MonoBehaviour
    {
        public delegate void SelectableEntityHandler(SelectableEntity selectableEntity);
        public event SelectableEntityHandler EntityDeselectedEvent;

        public virtual void OnSelected()
        {
            Debug.Log("Selected " + gameObject.name);
        }

        public virtual void OnDeselected()
        {
            Debug.Log("Deselected " + gameObject.name);
        }

        private void OnDisable()
        {
            EntityDeselectedEvent?.Invoke(this);
        }
    }
}
