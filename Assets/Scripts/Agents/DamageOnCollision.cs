using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator
{
    public class DamageOnCollision : MonoBehaviour
    {
        [SerializeField]
        private int _damageValue = 1;

        public int DamageValue { get => _damageValue; }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent<Damageable>(out Damageable damageable))
            {
                damageable.TakeDamage(_damageValue);
            }
        }
    }
}
