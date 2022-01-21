using System;
using _Root.Scripts.Views;
using UnityEngine;

namespace _Root.Scripts.Controllers.Level
{
    public class PortalController : IDisposable
    {
        private PortalView _portalView;
        private PlayerController _playerController;
        private bool _isUnlocked = true;

        public PortalController(PortalView portalView, PlayerController playerController)
        {
            _portalView = portalView;
            _portalView.OnPortalStay += MakeTeleportActive;
            _portalView.OnDestroyAction += Dispose;
            _playerController = playerController;
            _portalView.OnPortalStay += _playerController.MakeAlowed;
        }

        private void MakeTeleportActive()
        {
            if (_isUnlocked)
            {
                _isUnlocked = false;
                _playerController.Teleportation += Teleport;
            }
            else
            {
                _isUnlocked = true;
                _playerController.Teleportation -= Teleport;
            }
        }

        private void Teleport(Collider2D obj)
        {
            obj.gameObject.transform.position = _portalView.ConnectedPortal.transform.position;
        }


        public void Dispose()
        {
            _playerController.Teleportation -= Teleport;
            _portalView.OnDestroyAction -= Dispose;
        }
    }
}