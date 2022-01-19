using System.Collections.Generic;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;


namespace _Root.Scripts.Controllers.Obstacles
{
    public class ObstacleFactory
    {
        #region Fields

        private readonly List<ObstacleView> _obstacleViews;
        private readonly ExecutableObjects _executableObjects;

        #endregion


        #region Constructor

        public ObstacleFactory(List<ObstacleView> obstacleViews, ExecutableObjects executableObjects)
        {
            _obstacleViews = obstacleViews;
            _executableObjects = executableObjects;
        }

        #endregion


        #region Methods

        public List<ObstacleController> CreateObstacles()
        {
            List<ObstacleController> obstacleControllers = new List<ObstacleController>();
            for (int i = 0; i < _obstacleViews.Count; i++)
            {
                var model = new ObstacleModel(_obstacleViews[i].ObstacleConfig.Damage,_obstacleViews[i].ObstacleConfig.ObstacleType,
                    _obstacleViews[i].ObstacleConfig.DamageType,
                    _obstacleViews[i].ObstacleConfig.Cooldown, _obstacleViews[i].ObstacleConfig.Duration);
                ObstacleController obstacleController = new NullObstacleController(_obstacleViews[i], model);
                switch (_obstacleViews[i].ObstacleConfig.ObstacleType)
                {
                    case ObstacleType.Spike:
                        obstacleController = new SpikeController(_obstacleViews[i], model);
                        break;
                    case ObstacleType.Pike:
                        obstacleController = new PikeController(_obstacleViews[i], model);
                        break;
                    case ObstacleType.Saw:
                        obstacleController = new SawController(_obstacleViews[i], model, _obstacleViews[i].ObstacleConfig.Distance);
                        break;
                    case ObstacleType.Flamethrower:
                        obstacleController = new FlamethrowerController(_obstacleViews[i], model);
                        _executableObjects.AddExecutable(obstacleController);
                        break;
                }
                
                obstacleControllers.Add(obstacleController);
            }

            return obstacleControllers;
        }

        #endregion
        
    }
}