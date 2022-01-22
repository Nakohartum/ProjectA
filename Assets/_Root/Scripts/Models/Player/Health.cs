using UnityEngine;
using UnityEngine.Events;


namespace _Root.Scripts.Models
{
    public class Health
    {
        #region Properties

        private float MaxHP { get; set; }
        private float CurrentHP { get; set; }
        

        #endregion



        #region Constructor

        public Health(float maxHp)
        {
            MaxHP = maxHp;
            CurrentHP = MaxHP;
        }

        #endregion


        #region Methods

        public bool RemoveHealthPoints(float value, bool isUnTouchable)
        {
            bool result = false;

            if (!isUnTouchable)
            {
                CurrentHP -= value;
                result = true;
            }

            if (CurrentHP == 0 || CurrentHP < 0)
            {
                result = false;
            }

            return result;
        }

        public bool RemoveHealthPoints(float damage)
        {
            bool result = true;
            CurrentHP -= damage;
            if (CurrentHP <= 0)
            {
                result = false;
            }

            return result;
        }

        public void AddHealthPoints(float value)
        {
            CurrentHP += value;
            if (CurrentHP > MaxHP)
            {
                CurrentHP = MaxHP;
            }
        }

        #endregion
    }
}