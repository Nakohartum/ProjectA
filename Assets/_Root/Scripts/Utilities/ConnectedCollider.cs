using System;
using _Root.Scripts.Views;
using UnityEngine;


namespace _Root.Scripts.Utilities
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ConnectedCollider : MonoBehaviour
    {
        #region Fields

        [field: SerializeField] public Collider2D Collider { get; private set; }
        public event Action OnTriggerCollide = () => { };

        #endregion


        #region Unity Methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerCollide.Invoke();
        }

        #endregion
    }
}