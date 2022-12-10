using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator
{
    public class Agent : MonoBehaviour
    {
        public string Name { get; set; }

        public delegate void AgentHandler(Agent agent);
        public event AgentHandler OnAgentDeactivatedEvent;

        public void Spawn(Vector3 initialPosition)
        {
            GetComponent<Transform>().position = initialPosition;
            gameObject.SetActive(true);
        }

        private void Deactivate()
        {
            OnAgentDeactivatedEvent?.Invoke(this);
        }
    }
}
