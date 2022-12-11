using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgentSimulator.Agents
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private int _initialValue = 3;

        public int InitialValue { get => _initialValue; }
        public int CurrentValue { get => _currentValue; }

        public delegate void HealthHandler();
        public event HealthHandler OnHealthChangedEvent;
        public event HealthHandler OnHealthIsOverEvent;

        private int _currentValue;

        public void Initialize()
        {
            _currentValue = _initialValue;    
        }

        public void ChangeHealthValue(int amountToChange)
        {
            if (amountToChange == 0)
                return;

            _currentValue += amountToChange;

            OnHealthChangedEvent?.Invoke();

            if (_currentValue <= 0)
            {
                _currentValue = 0;

                OnHealthIsOverEvent?.Invoke();
            }
        }
    }
}
