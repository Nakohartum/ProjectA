using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Root.Scripts.Views
{
    public class CoinView : MonoBehaviour
    {
        [field: SerializeField] public int Amount { get; private set; }
        public UnityEvent<int> OnMoneyCollect = new UnityEvent<int>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnMoneyCollect.Invoke(Amount);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnMoneyCollect.RemoveAllListeners();
        }
    }
}