using System;
using UnityEngine;

namespace _Root.Scripts.Views
{
    public class PortalView : MonoBehaviour
    {
        [field: SerializeField] public PortalView ConnectedPortal { get; private set; }
        public event Action OnPortalStay = () => { };
        public bool IsOnTeleport { get; private set; }
        public event Action OnDestroyAction = () => { };

        private void OnTriggerEnter2D(Collider2D other)
        {
            IsOnTeleport = true;
            OnPortalStay.Invoke();
        }

        private void OnDestroy()
        {
            OnDestroyAction.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnPortalStay.Invoke();
            IsOnTeleport = false;
        }
    }
}