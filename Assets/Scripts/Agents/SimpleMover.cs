using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AgentSimulator
{
    public class SimpleMover : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1.0f;

        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_rigidbody == null)
                return;

            _rigidbody.velocity = _transform.forward * _speed;
        }
    }
}
