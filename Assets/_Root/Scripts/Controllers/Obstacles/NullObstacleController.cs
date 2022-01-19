using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;


namespace _Root.Scripts.Controllers.Obstacles
{
    public class NullObstacleController : ObstacleController
    {
        #region Constructor

        public NullObstacleController(ObstacleView obstacleView, IObstacleModel obstacleModel) : base(obstacleView, obstacleModel)
        {
        }

        #endregion


        #region Methods

        protected override void ApplyEffect()
        {
            
        }

        #endregion
        
    }
}