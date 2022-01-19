using _Root.Scripts.Controllers.Interfaces;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;
using UnityEngine.Events;


namespace _Root.Scripts.Controllers.Obstacles
{
    public abstract class ObstacleController : IExecutable
    {
        #region Fields
        
        protected readonly ObstacleView _obstacleView;
        protected readonly IObstacleModel _obstacleModel;

        public readonly UnityEvent<float> OnPlayerCollide = new UnityEvent<float>();

        #endregion


        #region Constructor

        protected ObstacleController(ObstacleView obstacleView, IObstacleModel obstacleModel)
        {
            _obstacleView = obstacleView;
            _obstacleModel = obstacleModel;
            _obstacleView.OnPlayerCollide += ApplyEffect;
            
        }


        #endregion
        
        
        #region MyRegion

        protected virtual void ApplyEffect()
        {
            OnPlayerCollide.Invoke(_obstacleModel.Damage);
        }
        
        public virtual void Execute(float deltaTime)
        {
            
        }

        #endregion


        
    }
}