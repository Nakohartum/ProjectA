using _Root.Scripts.Controllers.Interfaces;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;

namespace _Root.Scripts.Controllers.Obstacles
{
    public class FlamethrowerController : ObstacleController, IExecutable
    {
        private float _timeToRun;
        public FlamethrowerController(ObstacleView obstacleView, IObstacleModel obstacleModel) : base(obstacleView, obstacleModel)
        {
            _timeToRun = _obstacleModel.Cooldown;
        }

        private void ExploreFire()
        {
            _obstacleView.Particles.Simulate(_obstacleModel.Duration);
        }

        public void Execute(float deltaTime)
        {
            _timeToRun -= deltaTime;
            if (_timeToRun < 0)
            {
                ExploreFire();
            }
            if (_obstacleView.Particles.isPlaying)
            {
                _obstacleView.ConnectedCollider.Collider.enabled = false;
            }
            else
            {
                _obstacleView.ConnectedCollider.Collider.enabled = true;
            }
        }
    }
}