using System;
using UnityEngine;

namespace _Root.Scripts.Models
{
    public class Health
    {
        #region Properties

        public float MaxHP { get; private set; }
        public float CurrentHP { get; private set; }
        public event Action OnHPEnded = () => { }; 
        public event Action OnHPChange = () => { }; 

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
            CurrentHP -= value;
            
            Debug.Log($"Current {CurrentHP}");
            if (CurrentHP == 0 || CurrentHP < 0)
            {
                OnHPEnded.Invoke();
            }
            OnHPChange.Invoke();
        }

        public void AddHealthPoints(float value)
        {
            CurrentHP += value;
        }

        #endregion
    }
}