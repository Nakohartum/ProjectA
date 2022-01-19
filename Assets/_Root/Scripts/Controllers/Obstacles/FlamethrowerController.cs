using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;


namespace _Root.Scripts.Controllers.Obstacles
{
    public class FlamethrowerController : ObstacleController
    {
        #region Fields
        
        private float _timeToRun;

        #endregion


        #region Constructor

        public FlamethrowerController(ObstacleView obstacleView, IObstacleModel obstacleModel) : base(obstacleView, obstacleModel)
        {
            _timeToRun = _obstacleModel.Cooldown;
            _obstacleView.Particles.Stop();
            var particlesMain = _obstacleView.Particles.main;
            particlesMain.duration = _obstacleModel.Duration;
        }

        #endregion


        #region Methods

        private void ExploreFire()
        {
            _obstacleView.Particles.Play();
        }

        public override void Execute(float deltaTime)
        {
            _timeToRun -= deltaTime;
            if (_timeToRun < 0)
            {
                ExploreFire();
                _timeToRun = _obstacleModel.Cooldown;
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