using System.Collections;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;
using DG.Tweening;
using UnityEngine;

namespace _Root.Scripts.Controllers.Obstacles
{
    public class PikeController : ObstacleController
    {
        #region Fields

        private float _waitTime;

        #endregion
        
        #region Constructor

        public PikeController(ObstacleView obstacleView, IObstacleModel obstacleModel) : base(obstacleView, obstacleModel)
        {
            _obstacleView.ConnectedCollider.OnTriggerCollide += PlayAnimation;
        }

        #endregion


        #region Methods

        private void PlayAnimation()
        {
            SetTrapActive();
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_obstacleView.gameObject.transform.DOMove(new Vector3(
                _obstacleView.gameObject.transform.position.x,
                _obstacleView.gameObject.transform.position.y + 1.5f), 0.5f));
            sequence.Append(_obstacleView.gameObject.transform.DOMove(new Vector3(
                _obstacleView.gameObject.transform.position.x,
                _obstacleView.gameObject.transform.position.y + 1.5f), _obstacleModel.Cooldown));
            sequence.Append(_obstacleView.gameObject.transform.DOMove(new Vector3(
                _obstacleView.gameObject.transform.position.x,
                _obstacleView.gameObject.transform.position.y), 0.5f)).OnComplete(SetTrapActive);

        }
        

        private void SetTrapActive()
        {
            if (_obstacleView.ConnectedCollider.gameObject.activeSelf)
            {
                _obstacleView.ConnectedCollider.gameObject.SetActive(false);
            }
            else
            {
                _obstacleView.ConnectedCollider.gameObject.SetActive(true);
            }
        }

        #endregion
        
        
    }
}