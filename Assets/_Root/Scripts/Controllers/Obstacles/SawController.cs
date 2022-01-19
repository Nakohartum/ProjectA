using _Root.Scripts.Controllers.Interfaces;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;
using DG.Tweening;
using UnityEngine;

namespace _Root.Scripts.Controllers.Obstacles
{
    public class SawController : ObstacleController
    {
        #region Fields

        private float _distanceToRush;
        private Vector2 _startPos;

        #endregion
        public SawController(ObstacleView obstacleView, IObstacleModel obstacleModel, float distanceToRush) : base(obstacleView, obstacleModel)
        {
            _distanceToRush = distanceToRush;
            _startPos = _obstacleView.transform.position;
            _obstacleView.OnStart += StartAnimation;
        }

        private void StartAgain()
        {
            StartAnimation();
        }

        public void StartAnimation()
        {
            Debug.Log("Start");
            var vectorMove = _startPos;
            vectorMove.x = _distanceToRush;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_obstacleView.transform.DOMove(vectorMove, _obstacleModel.Duration).SetEase(Ease.Linear));
            sequence.Append(_obstacleView.transform.DOMove(vectorMove, _obstacleModel.Cooldown));
            sequence.Append(_obstacleView.transform.DOMove(_startPos, _obstacleModel.Duration).SetEase(Ease.Linear));
            sequence.Append(_obstacleView.transform.DOMove(_startPos, _obstacleModel.Cooldown)).OnComplete(StartAgain);
            
        }

    }
}