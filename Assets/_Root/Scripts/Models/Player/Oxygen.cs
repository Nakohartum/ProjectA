using UnityEngine;
using UnityEngine.Events;

namespace _Root.Scripts.Controllers
{
    public class Oxygen
    {
        #region Fields

        private readonly float _timeToRun;
        private float _timeLeft;

        #endregion
        
        #region Properties

        private float MaxOxygen { get; set; }
        private float CurrentOxygen { get; set; }
        public bool HasOxygen { get; private set; } = true;

        #endregion


        #region Constructor

        public Oxygen(float maxOxygen)
        {
            MaxOxygen = maxOxygen;
            CurrentOxygen = MaxOxygen;
            _timeToRun = 1f;
            _timeLeft = _timeToRun;
        }

        #endregion


        #region Methods

        public void RemoveOxygen(float deltaTime)
        {
            
            _timeLeft -= deltaTime;
            if (_timeLeft < 0)
            {
                _timeLeft = _timeToRun;
                CurrentOxygen -= 1;
            }
            if (CurrentOxygen == 0)
            {
                HasOxygen = false;
            }
        }


        public bool RemoveAmountOfOxygen(float amount, bool isUnTouchable)
        {
            bool res = false;
            if (!isUnTouchable)
            {
                CurrentOxygen -= amount;
                res = true;
            }

            if (CurrentOxygen == 0 || CurrentOxygen < 0)
            {
                HasOxygen = false;
            }

            return res;
        }
        
        public void AddOxygen(float value)
        {
            CurrentOxygen += value;
            if (CurrentOxygen > MaxOxygen)
            {
                CurrentOxygen = MaxOxygen;
            }
        }

        #endregion
    }
}