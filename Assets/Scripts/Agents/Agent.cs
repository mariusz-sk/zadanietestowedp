using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.Agents
{
    // Agents are created by AgentSpawner by reusing objects from some kind of objects pool instantiated at start.
    // Agent objects are never destroyed. Instead we deactivate them (returning to the pool) and we treat this as they were being killed.

    public class Agent : MonoBehaviour
    {
        public delegate void AgentHandler(Agent agent);
        public event AgentHandler OnKilledEvent;

        private Health _health;

        public void Spawn(Vector3 initialPosition, string instanceName)
        {
            GetComponent<Transform>().position = initialPosition;
            gameObject.SetActive(true);
            gameObject.name = instanceName;

            if (_health != null)
            {
                _health.Initialize();
            }
        }

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            if (_health != null)
            {
                _health.OnHealthIsOverEvent += OnHealthIsOver;
            }
        }

        private void OnDisable()
        {
            if (_health != null)
            {
                _health.OnHealthIsOverEvent -= OnHealthIsOver;
            }
        }

        private void OnHealthIsOver()
        {
            // When your health is over deactivate yourself (instead of destroing)
            Deactivate();
        }

        private void Deactivate()
        {
            // Tell everyone interested that this agent has been killed
            OnKilledEvent?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
