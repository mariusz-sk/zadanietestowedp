using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AgentSimulator.Agents
{
    public class SimpleMover : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1.0f;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_rigidbody == null)
                return;

            _rigidbody.velocity = Vector3.forward * _speed;
        }
    }
}
