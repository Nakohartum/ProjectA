using System;
using _Root.Scripts.Models.Buffs;
using UnityEngine;

namespace _Root.Scripts.Views
{
    public class BuffView : MonoBehaviour
    {
        [field: SerializeField] public BuffConfig BuffConfig { get; private set; }
        public event Action OnPlayerCollide = () => { };
        public event Action OnObjectDestroy = () => { };

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnPlayerCollide.Invoke();
        }

        private void OnDestroy()
        {
            OnObjectDestroy.Invoke();
        }
    }
}