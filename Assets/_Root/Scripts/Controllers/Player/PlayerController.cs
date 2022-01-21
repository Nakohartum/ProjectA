using System;
using System.Collections.Generic;
using _Root.Scripts.Controllers.Interfaces;
using _Root.Scripts.Models;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;


namespace _Root.Scripts.Controllers
{
    public class PlayerController : IExecutable, IDisposable
    {
        #region Fields

        private readonly IPlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly PlayerInputController _playerInputController;
        private readonly ExecutableObjects _executableObjects;
        private bool _blockControllers;
        private bool _isUntouchable;
        private bool _playAnimation = true;
        private float DASH_TIMER;
        private float BLOCK_TIMER;
        private float _currentDashTimer;
        private float _currentBlockTime;
        private float _damageDelay = 1f;
        private float _currentTimeDelay;
        private bool _isAlowedToTP;
        public event Action<Collider2D> Teleportation = coll => {}; 

        #endregion

        
        #region Constructor

        public PlayerController(PlayerView playerView, IPlayerModel playerModel,
            PlayerInputController playerInputController, ExecutableObjects executableObjects)
        {
            _playerView = playerView;
            _playerModel = playerModel;
            _playerInputController = playerInputController;
            _executableObjects = executableObjects;
            DASH_TIMER = _playerModel.DashTime;
            BLOCK_TIMER = _playerModel.BlockTime;
            _currentBlockTime = BLOCK_TIMER;
            _currentDashTimer = DASH_TIMER;
            _currentTimeDelay = _damageDelay;
        }

        

        #endregion

        
        #region Methods

        public void ApplyEffects(float damage, DamageType damageType)
        {
            if (damageType == DamageType.Health) 
            {
                if (_playerModel.Health.RemoveHealthPoints(damage, _isUntouchable))
                {
                    _isUntouchable = true;
                }
                if (_isUntouchable && _playAnimation)
                {
                    _playAnimation = false;
                    Fading();
                }
                else if (!_isUntouchable)
                {
                    Dispose();
                }
            }
            else if (damageType == DamageType.Oxygen)
            {
                _playerModel.Oxygen.RemoveAmountOfOxygen(damage, _isUntouchable);
            }

        }

        public void MakeAlowed()
        {
            _isAlowedToTP = !_isAlowedToTP;
        }
        
        private void Bleeding()
        {
            _playerView.Particles.Play();
        }

        private void Fading()
        {
            _playAnimation = false;
            Bleeding();
            Sequence sequence = DOTween.Sequence();
            sequence.Append( _playerView.Renderer.DOFade(0.5f, 0.5f));
            sequence.Append( _playerView.Renderer.DOFade(1f, 0.5f));
            sequence.Append(_playerView.Renderer.DOFade(0.5f, 0.5f));
            sequence.Append( _playerView.Renderer.DOFade(1f, 0.5f));
            sequence.Append( _playerView.Renderer.DOFade(0.5f, 0.5f));
            sequence.Append( _playerView.Renderer.DOFade(1f, 0.5f));
            sequence.Append(_playerView.Renderer.DOFade(0.5f, 0.5f));
            sequence.Append( _playerView.Renderer.DOFade(1f, 0.5f)).OnComplete(MakeTouchable);
            sequence.Play();
        }
        
        private void MakeTouchable()
        {
            _isUntouchable = false;
            _playAnimation = true;
        }
        
        
        public void Execute(float deltaTime)
        {
            _currentTimeDelay -= deltaTime;
            _playerModel.Oxygen.RemoveOxygen(deltaTime);
            if (!_playerModel.Oxygen.HasOxygen && _currentTimeDelay < 0)
            {
                _currentTimeDelay = _damageDelay;
                if (!_playerModel.Health.RemoveHealthPoints(1))
                {
                    Dispose();
                }
            }

            if (Input.GetButtonDown("Use"))
            {
                
                if (_isAlowedToTP)
                {
                    Teleportation.Invoke(_playerView.Collider);
                }
            }
            if (!Input.GetButton("Dash") && _currentDashTimer != DASH_TIMER)
            {
                _currentDashTimer = DASH_TIMER;
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
                    _currentBlockTime -= deltaTime;
                    if (_currentBlockTime < 0)
                    {
                        _currentBlockTime = BLOCK_TIMER;
                        _blockControllers = false;
                    }
                    return;
                }
                _currentDashTimer -= deltaTime;
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
        
        
        public void Dispose()
        {
            _executableObjects.RemoveExecutable(this);
            _playerInputController.Dispose();
            
            Object.Destroy(_playerView.gameObject);
        }

        #endregion

        public void BlockJump(bool obj)
        {
            _playerInputController.ChangeJumpAccess(obj);
        }
    }
}