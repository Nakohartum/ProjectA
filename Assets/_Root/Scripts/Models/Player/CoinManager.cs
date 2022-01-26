using UnityEngine;

namespace _Root.Scripts.Models.Player
{
    public class CoinManager
    {
        private int _coinsAmount;

        public void AddMoney(int value)
        {
            _coinsAmount += value;
            Debug.Log($"Coins now {_coinsAmount}");
        }

        public bool Buy(int value)
        {
            if (_coinsAmount < value)
            {
                return false;
            }

            _coinsAmount -= value;
            return true;
        }
    }
}