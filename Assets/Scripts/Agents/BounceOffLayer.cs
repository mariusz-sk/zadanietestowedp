using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator
{
    public class BounceOffLayer : MonoBehaviour
    {
        public LayerMask layersToBounceOff;

        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (((1 << collision.gameObject.layer) & layersToBounceOff.value) == 0)
                return;

            ContactPoint contactPoint = collision.GetContact(0);

            Vector3 newMoveDirection = CalculateBounceVector(_transform.forward, contactPoint);

            _transform.rotation = Quaternion.LookRotation(newMoveDirection, Vector3.up);
        }

        private Vector3 CalculateBounceVector(Vector3 moveDirection, ContactPoint contactPoint)
        {
            Vector3 newDirection = moveDirection;

            Vector3 normal = contactPoint.normal;
            if (Vector3.Dot(moveDirection, normal) < 0.0f)
            {
                normal.y = 0.0f;
                newDirection = Vector3.Reflect(moveDirection, normal.normalized);
            }

            return newDirection;
        }
    }
}
