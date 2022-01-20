using System;
using _Root.Scripts.Controllers.Interfaces;
using _Root.Scripts.Views;
using DG.Tweening;
using UnityEngine;


namespace _Root.Scripts.Controllers.Camera
{
    public class CameraController : IExecutable, IDisposable
    {
        #region Fields

        private readonly CameraView _cameraView;
        private readonly Transform _targetTransform;
        private readonly ExecutableObjects _executableObjects;
        private readonly PlayerController _playerController;
        private readonly float _minXPosition;
        private readonly float _maxXPosition;
        private readonly float _minYPosition;
        private readonly float _maxYPosition;

        #endregion

        
        #region Constructor

        public CameraController(CameraView cameraView, ExecutableObjects executableObjects, PlayerController playerController)
        {
            _cameraView = cameraView;
            _targetTransform = _cameraView.TargetPosition;
            _executableObjects = executableObjects;
            _playerController = playerController;
            playerController.Teleportation += Flashing;
            _minXPosition = _cameraView.MinXPosition;
            _maxXPosition = _cameraView.MaxXPosition;
            _minYPosition = _cameraView.MinYPosition;
            _maxYPosition = _cameraView.MaxYPosition;
        }

        private void Flashing(Collider2D obj)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_cameraView.FlashingImage.DOFade(1f, 0f));
            sequence.Append(_cameraView.FlashingImage.DOFade(0f, 1f));
            sequence.Play();
        }

        #endregion

        
        #region Methods

        public void Execute(float deltaTime)
        {
            if (_targetTransform == null)
            {
                Dispose();
                return;
            }
            var pos = _cameraView.transform.position;
            var position = _targetTransform.position;
            pos.x = Mathf.Clamp(position.x, _minXPosition, _maxXPosition);
            pos.y = Mathf.Clamp(position.y, _minYPosition, _maxYPosition);
            _cameraView.transform.position = pos;
        }

        #endregion


        public void Dispose()
        {
            _executableObjects.RemoveExecutable(this);
        }
    }
}