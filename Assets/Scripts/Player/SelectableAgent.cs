using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.Player
{
    public class SelectableAgent : MonoBehaviour
    {
        public delegate void SelectableAgentHandler(SelectableAgent selectableAgent);
        public event SelectableAgentHandler AgentDeselectedEvent;

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
            AgentDeselectedEvent?.Invoke(this);
        }
    }
}
