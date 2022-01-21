using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;
using DG.Tweening;
using UnityEngine;


namespace _Root.Scripts.Controllers.Obstacles
{
    public class SawController : ObstacleController
    {
        #region Fields

        private readonly float _distanceToRush;
        private readonly Vector2 _startPos;
        private int _isWorkingHash;

        #endregion

        #region Constructor

        public SawController(ObstacleView obstacleView, IObstacleModel obstacleModel, float distanceToRush) : base(obstacleView, obstacleModel)
        {
            _distanceToRush = distanceToRush;
            _startPos = _obstacleView.transform.position;
            _obstacleView.OnStart += StartAnimation;
            _isWorkingHash = Animator.StringToHash("IsWorking");
        }

        #endregion


        #region Methods

        private void StartAgain()
        {
            StartAnimation();
        }

        public void StartAnimation()
        {
            var vectorMove = _startPos;
            vectorMove.x = _distanceToRush;
            Sequence sequence = DOTween.Sequence();
            MakeIdle();
            sequence.Append(_obstacleView.transform.DOMove(vectorMove, _obstacleModel.Duration).SetEase(Ease.Linear).OnComplete(MakeIdle));
            sequence.Append(_obstacleView.transform.DOMove(vectorMove, _obstacleModel.Cooldown).OnComplete(MakeIdle));
            sequence.Append(_obstacleView.transform.DOMove(_startPos, _obstacleModel.Duration).SetEase(Ease.Linear).OnComplete(MakeIdle));
            sequence.Append(_obstacleView.transform.DOMove(_startPos, _obstacleModel.Cooldown)).OnComplete(StartAgain);
            
        }

        private void MakeIdle()
        {
            _obstacleView.Animator.SetBool(_isWorkingHash, !_obstacleView.Animator.GetBool(_isWorkingHash));
        }

        #endregion
        

    }
}