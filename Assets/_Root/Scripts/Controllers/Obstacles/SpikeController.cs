using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;


namespace _Root.Scripts.Controllers.Obstacles
{
    public class SpikeController : ObstacleController
    {
        #region Constructor

        public SpikeController(ObstacleView obstacleView, IObstacleModel obstacleModel) : base(obstacleView, obstacleModel)
        {
        }

        #endregion
        
    }
}