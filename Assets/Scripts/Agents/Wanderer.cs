using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator
{
    public class Wanderer : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed = 45.0f;

        [SerializeField, Range(0.0f, 90.0f)]
        private float _directionAngleStep = 15.0f;

        [SerializeField, Range(0.0f, 60.0f)]
        private float _changeDirectionDelay = 2.0f;


        private Transform _transform;

        private float _nextChangeDirectionTime;
        private Vector3 _targetDirection;


        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void OnEnable()
        {
            CalculateNewOrientation();
            CalculateNextChangeDirectionTime();
        }

        private void Update()
        {
            Vector3 newDirection = Vector3.RotateTowards(_transform.forward, _targetDirection, _rotationSpeed * Time.deltaTime * Mathf.Deg2Rad, 1.0f);
            
            _transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);

            if (Time.timeSinceLevelLoad >= _nextChangeDirectionTime)
            {
                CalculateNewOrientation();

                CalculateNextChangeDirectionTime();
            }
        }

        private void CalculateNextChangeDirectionTime()
        {
            _nextChangeDirectionTime = Time.timeSinceLevelLoad + _changeDirectionDelay;
        }

        private void CalculateNewOrientation()
        {
            float randomAngle = Random.value * 360.0f;
            if (_directionAngleStep > 0.0f)
            {
                randomAngle = Mathf.Round(randomAngle / _directionAngleStep) * _directionAngleStep;
            }

            Quaternion randomAngleRotation = Quaternion.AngleAxis(randomAngle, Vector3.up);

            _targetDirection = randomAngleRotation * Vector3.forward;
        }

        private void OnDrawGizmosSelected()
        {
            if (_transform == null)
                return;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_transform.position, _transform.position + _targetDirection * 2.0f);
        }
    }
}
