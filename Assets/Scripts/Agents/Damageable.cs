using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator
{
    public class Damageable : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        public void TakeDamage(int amount)
        {
            if (_health == null)
                return;

            _health.ChangeHealthValue(-amount);
        }
    }
}
