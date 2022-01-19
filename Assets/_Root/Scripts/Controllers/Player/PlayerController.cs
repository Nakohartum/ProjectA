using System;
using System.Collections;
using _Root.Scripts.Controllers.Interfaces;
using _Root.Scripts.Models;
using _Root.Scripts.Views;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Root.Scripts.Controllers
{
    public class PlayerController : IExecutable, IDisposable
    {
        #region Fields

        private IPlayerModel _playerModel;
        private PlayerView _playerView;
        private PlayerInputController _playerInputController;
        private ExecutableObjects _executableObjects;
        private bool _blockControllers;
        private float DASH_TIMER;
        private float BLOCK_TIMER;
        private float _currentDashTimer;
        private float _currentBlockTime;

        #endregion

        
        #region Constructor

        public PlayerController(PlayerView playerView, IPlayerModel playerModel,
            PlayerInputController playerInputController, ExecutableObjects executableObjects)
        {
            _playerView = playerView;
            _playerModel = playerModel;
            _playerInputController = playerInputController;
            _executableObjects = executableObjects;
            _playerView.StartCoroutine(MinusOxygen());
            _playerModel.Health.OnHPEnded += Dispose;
            _playerModel.Health.OnHPChange += HPChangeAnimation;
            DASH_TIMER = _playerModel.DashTime;
            BLOCK_TIMER = _playerModel.BlockTime;
            _currentBlockTime = BLOCK_TIMER;
            _currentDashTimer = DASH_TIMER;
        }

        private void HPChangeAnimation()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append( _playerView.Renderer.DOFade(0.5f, 0.5f));
            sequence.Append( _playerView.Renderer.DOFade(1f, 0.5f));
            sequence.Append(_playerView.Renderer.DOFade(0.5f, 0.5f));
            sequence.Append( _playerView.Renderer.DOFade(1f, 0.5f));
            sequence.Append( _playerView.Renderer.DOFade(0.5f, 0.5f));
            sequence.Append( _playerView.Renderer.DOFade(1f, 0.5f));
            sequence.Append(_playerView.Renderer.DOFade(0.5f, 0.5f));
            sequence.Append( _playerView.Renderer.DOFade(1f, 0.5f));
            sequence.Play();
        }

        #endregion

        
        #region Methods

        public void Execute(float deltaTime)
        {
            if (!Input.GetButton("Dash") && _currentDashTimer != DASH_TIMER)
            {
                _currentDashTimer = DASH_TIMER;
                Debug.Log("Ready to dash");
            }
            if (_currentDashTimer < 0)
            {
                _currentDashTimer = DASH_TIMER;
                _blockControllers = true;
            }
            Dash(deltaTime);
            _playerInputController.Move(deltaTime);
        }

        
        private void Dash(float deltaTime)
        {
            if (Input.GetButton("Dash"))
            {
                if (_blockControllers)
                {
                    Debug.Log("Can't dash");
                    _currentBlockTime -= deltaTime;
                    if (_currentBlockTime < 0)
                    {
                        _currentBlockTime = BLOCK_TIMER;
                        _blockControllers = false;
                    }
                    return;
                }
                _currentDashTimer -= deltaTime;
                Debug.Log("Ready to dash");
                if (_playerView.Renderer.flipX)
                {
                    _playerInputController.Dash(-_playerModel.DashPower);
                }
                else
                {
                    _playerInputController.Dash(_playerModel.DashPower);
                }
            }
        }
        
        private IEnumerator MinusOxygen()
        {
            while (_playerModel.Oxygen.HasOxygen)
            {
                yield return new WaitForSeconds(1f);
                _playerModel.Oxygen.RemoveOxygen(1f);
            }
            _playerView.StopCoroutine(MinusOxygen());
        }
        public void Dispose()
        {
            _executableObjects.RemoveExecutable(this);
            _playerInputController.Dispose();
            _playerModel.Health.OnHPEnded -= Dispose;
            _playerModel.Health.OnHPChange -= HPChangeAnimation;
            Object.Destroy(_playerView.gameObject);
        }

        #endregion
    }
}