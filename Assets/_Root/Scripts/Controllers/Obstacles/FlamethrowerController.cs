using System;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;
using UnityEngine;


namespace _Root.Scripts.Controllers.Obstacles
{
    public class FlamethrowerController : ObstacleController
    {
        #region Fields
        
        private float _timeToRun;
        private int _isReadyToShootHash;
        private bool _isShooting = true;
        private float _timer;

        #endregion


        #region Constructor

        public FlamethrowerController(ObstacleView obstacleView, IObstacleModel obstacleModel) : base(obstacleView, obstacleModel)
        {
            _timeToRun = _obstacleModel.Cooldown;
            _obstacleView.Particles.Stop();
            var particlesMain = _obstacleView.Particles.main;
            _isReadyToShootHash = Animator.StringToHash("IsReadyToFire");
            particlesMain.duration = _obstacleModel.Duration;
            _timer = _obstacleModel.Duration;
            _obstacleView.OnAnimationEnds += ExploreFire;
        }

        #endregion


        #region Methods

        private void ExploreFire()
        {
            if (!_obstacleView.Particles.isPlaying)
            {
                _obstacleView.Particles.Play();
            }
            
        }
        

        public override void Execute(float deltaTime)
        {
            if (_isShooting)
            {
                _timer -= deltaTime;
                if (_timer < 0)
                {
                    _timer = _obstacleModel.Duration;
                    _obstacleView.Particles.Stop();
                    _isShooting = false;
                }
            }
            else
            {
                if (_timeToRun < 0)
                {
                    _isShooting = true;
                }
            }

            if (_isShooting)
            {
                _obstacleView.Animator.SetBool(_isReadyToShootHash, true);
                _timeToRun = _obstacleModel.Cooldown;
            }
            else
            {
                _obstacleView.Animator.SetBool(_isReadyToShootHash, false);
                _timeToRun -= deltaTime;
            }
            if (_obstacleView.Particles.isPlaying)
            {
                _obstacleView.ConnectedCollider.Collider.enabled = true;
            }
            else
            {
                _obstacleView.ConnectedCollider.Collider.enabled = false;
            }
        }

        #endregion
        
    }
}