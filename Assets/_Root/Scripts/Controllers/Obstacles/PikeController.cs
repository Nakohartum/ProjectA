using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;
using DG.Tweening;
using UnityEngine;


namespace _Root.Scripts.Controllers.Obstacles
{
    public class PikeController : ObstacleController
    {

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
            var position = _obstacleView.gameObject.transform.position;
            sequence.Append(_obstacleView.gameObject.transform.DOMove(new Vector3(
                position.x,
                position.y + 1.5f), 0.5f));
            sequence.Append(_obstacleView.gameObject.transform.DOMove(new Vector3(
                position.x,
                position.y + 1.5f), _obstacleModel.Cooldown));
            sequence.Append(_obstacleView.gameObject.transform.DOMove(new Vector3(
                position.x,
                position.y), 0.5f)).OnComplete(SetTrapActive);

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