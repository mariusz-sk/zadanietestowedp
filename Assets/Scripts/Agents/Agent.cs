using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.Agents
{
    public class Agent : MonoBehaviour
    {
        public string Name { get; set; }

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
            Deactivate();
        }

        private void Deactivate()
        {
            OnKilledEvent?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
