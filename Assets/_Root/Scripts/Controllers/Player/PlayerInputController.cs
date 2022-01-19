using System;
using _Root.Scripts.Controllers.Interfaces;
using _Root.Scripts.Models;
using _Root.Scripts.Views;
using UnityEngine;

namespace _Root.Scripts.Controllers
{
    public class PlayerInputController : IDisposable
    {
        #region Fields

        private PlayerView _playerView;
        private ContactPoller _contactPoller;
        private IPlayerModel _playerModel;
        private Rigidbody2D _rigidbody;
        private const int JUMP_WALL_COEF = 5;
        private const int JUMP_VALUE_ITERATIONS = 60;
        private PlayerHorizontalInput _horizontalInput;
        private PlayerJumpController _jumpController;
        private bool _jumpControl;
        private int _jumpIterations = 0;
        private int _jumpCount = 0;
        private int _jumpHash;
        private int _runHash;
        private int _onWallHash;
        private int _onWallAnimHash;
        private float _horizontalMove;
        private float _jumpAxis;

        #endregion


        #region Constructor

        public PlayerInputController(IPlayerModel playerModel, Rigidbody2D rigidbody, 
            ContactPoller contactPoller, PlayerView playerView)
        {
            _playerModel = playerModel;
            _rigidbody = rigidbody;
            _contactPoller = contactPoller;
            _playerView = playerView;
            _jumpHash = Animator.StringToHash("IsJumping");
            _runHash = Animator.StringToHash("Speed");
            _onWallHash = Animator.StringToHash("IsOnWall");
            _onWallAnimHash = Animator.StringToHash("OnWall");
            _horizontalInput = new PlayerHorizontalInput();
            _jumpController = new PlayerJumpController();
            _horizontalInput.OnAxisChange += HorizontalInputOnOnAxisChange;
            _jumpController.OnAxisChange += JumpControllerOnOnAxisChange;
            _horizontalMove = 0;
        }

        #endregion


        #region Methods

        public void Move(float deltaTime)
        {
            _contactPoller.UpdateContacts();
            _horizontalInput.Execute(deltaTime);
            _jumpController.Execute(deltaTime);
            Vector3 vectorMove = new Vector3(_horizontalMove * _playerModel.Speed, _rigidbody.velocity.y);
            _rigidbody.velocity = vectorMove; 
            Reflect();
            Jump();
            JumpFromWall();
            PlayAnimation();
        }

        private void JumpControllerOnOnAxisChange(float obj)
        {
            _jumpAxis = obj;
        }

        private void HorizontalInputOnOnAxisChange(float obj)
        {
            _horizontalMove = obj;
        }


        public void Dash(float dashPower)
        {
            _rigidbody.AddForce(Vector2.right * dashPower);
        }

        private void PlayAnimation()
        {
            _playerView.Animator.SetBool(_jumpHash, !_contactPoller.IsGrounded);
            if (_horizontalMove > 0.1 || _horizontalMove < -0.1)
            {
                _playerView.Animator.SetFloat(_runHash, 1);
            }
            else
            {
                _playerView.Animator.SetFloat(_runHash, 0);
            }
        }

        private void Jump()
        {
            if (_jumpAxis > 0.1f)
            {
                if (_contactPoller.IsGrounded)
                {
                    _jumpControl = true;
                }
            }
            else
            {
                _jumpControl = false;
            }

            if (_jumpControl)
            {
                if (_jumpIterations++ < JUMP_VALUE_ITERATIONS)
                {
                    _rigidbody.AddForce(Vector2.up * _playerModel.JumpSpeed / _jumpIterations);
                }
            }
            else
            {
                _jumpIterations = 0;
            }

        }

        private void JumpFromWall()
        {
            if ((_contactPoller.HasLeftContact || _contactPoller.HasRightContact) && !_contactPoller.IsGrounded)
            {
                _playerView.Animator.SetBool(_onWallHash, true);
                _playerView.Animator.StopPlayback();
                _playerView.Animator.Play(_onWallAnimHash);
                if (_horizontalMove == 0)
                {
                    _rigidbody.gravityScale = 0;
                    _rigidbody.velocity = Vector2.zero;
                }
                if (Input.GetButtonDown("Jump"))
                {
                    _rigidbody.AddForce(Vector2.up * _playerModel.JumpSpeed * JUMP_WALL_COEF);
                }
            }
            else
            {
                _rigidbody.gravityScale = 1;
                _playerView.Animator.SetBool(_onWallHash, false);
            }
        }

        private void Reflect()
        {
            if (_horizontalMove > 0)
            {
                _playerView.Renderer.flipX = false;
            }
            else if (_horizontalMove < 0)
            {
                _playerView.Renderer.flipX = true;
            }
        }
        
        public void Dispose()
        {
            _horizontalInput.OnAxisChange -= HorizontalInputOnOnAxisChange;
            _jumpController.OnAxisChange -= JumpControllerOnOnAxisChange;
        }

        #endregion
    }
}