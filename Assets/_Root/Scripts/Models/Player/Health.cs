using UnityEngine;
using UnityEngine.Events;


namespace _Root.Scripts.Models
{
    public class Health
    {
        #region Properties

        private float MaxHP { get; set; }
        private float CurrentHP { get; set; }
        public readonly UnityEvent OnHPEnded = new UnityEvent();
        public readonly UnityEvent OnHPChange = new UnityEvent();
        private bool _isUntouchable;

        #endregion



        #region Constructor

        public Health(float maxHp)
        {
            MaxHP = maxHp;
            CurrentHP = MaxHP;
        }

        #endregion


        #region Methods

        public void RemoveHealthPoints(float value)
        {
            if (_isUntouchable == false)
            {
                ChangeTouchable(true);
                CurrentHP -= value;

                Debug.Log($"Current {CurrentHP}");
                if (CurrentHP == 0 || CurrentHP < 0)
                {
                    OnHPEnded.Invoke();
                }

                OnHPChange.Invoke();
            }
        }

        public void ChangeTouchable(bool value)
        {
            _isUntouchable = value;
        }
        
        public void AddHealthPoints(float value)
        {
            CurrentHP += value;
        }

        #endregion
    }
}