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
        
        public event Action<bool> OnCollide = b => { };

        

        #endregion


        #region Unity Methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerCollide.Invoke();
            OnCollide.Invoke(false);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnCollide.Invoke(true);
        }

        #endregion
    }
}