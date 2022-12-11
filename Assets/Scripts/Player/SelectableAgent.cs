using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.Player
{
    public class SelectableAgent : MonoBehaviour
    {
        [SerializeField]
        private GameObject _selectionEffect;

        public delegate void SelectableAgentHandler(SelectableAgent selectableAgent);
        public event SelectableAgentHandler AgentDeselectedEvent;

        public virtual void OnSelected()
        {
            //Debug.Log("Selected " + gameObject.name);
            if (_selectionEffect != null)
                _selectionEffect.SetActive(true);
        }

        public virtual void OnDeselected()
        {
            //Debug.Log("Deselected " + gameObject.name);
            if (_selectionEffect != null)
                _selectionEffect.SetActive(false);
        }

        private void Start()
        {
            if (_selectionEffect != null)
                _selectionEffect.SetActive(false);
        }

        private void OnDisable()
        {
            AgentDeselectedEvent?.Invoke(this);
        }
    }
}
