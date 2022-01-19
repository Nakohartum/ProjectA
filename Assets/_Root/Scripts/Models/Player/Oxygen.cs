using UnityEngine;

namespace _Root.Scripts.Controllers
{
    public class Oxygen
    {
        #region Fields

        private float _timeToRun;
        private float _timeLeft;

        #endregion
        
        #region Properties

        public float MaxOxygen { get; private set; }
        public float CurrentOxygen { get; set; }
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

        public void AddOxygen(float value)
        {
            if (CurrentOxygen > MaxOxygen || CurrentOxygen == MaxOxygen)
            {
                return;
            }
            CurrentOxygen += value;
            if (CurrentOxygen > MaxOxygen)
            {
                CurrentOxygen = MaxOxygen;
            }
        }

        #endregion
    }
}