using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.Agents
{
    // This component changes object orientation every given amount of time, set in the Inspector.
    // You can also set in the Inspector rotation speed and what step in angles it should rotate by.
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
            // New direction between current and target object orientation
            Vector3 newDirection = Vector3.RotateTowards(_transform.forward, _targetDirection, _rotationSpeed * Time.deltaTime * Mathf.Deg2Rad, 1.0f);
            
            _transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);

            // If it is time then generate next random orientation
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
            // Generate new random direction (0 - 360)
            float randomAngle = Random.value * 360.0f;
            
            // Snap this angle to nearest angle step - if angle step is set in the Inspector
            if (_directionAngleStep > 0.0f)
            {
                randomAngle = Mathf.Round(randomAngle / _directionAngleStep) * _directionAngleStep;
            }

            // Calculate direction vector from generated angle
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
