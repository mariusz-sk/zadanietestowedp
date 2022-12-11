using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.Agents
{
    public class DamageOnCollision : MonoBehaviour
    {
        [SerializeField]
        private int _damageValue = 1;

        public int DamageValue { get => _damageValue; }


        private void OnCollisionEnter(Collision collision)
        {
            var damageable = collision.collider.GetComponentInParent<Damageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_damageValue);
            }
        }
    }
}
