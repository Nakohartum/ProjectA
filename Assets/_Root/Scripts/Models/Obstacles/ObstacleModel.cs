namespace _Root.Scripts.Models.Obstacles
{
    public class ObstacleModel : IObstacleModel
    {
        #region Propetries

        public float Damage { get; }
        public float Cooldown { get; }
        
        public float Duration { get; }
        public ObstacleType ObstacleType { get; }

        #endregion


        #region Constructor

        public ObstacleModel(float damage, ObstacleType obstacleType, float cooldown, float duration)
        {
            Damage = damage;
            ObstacleType = obstacleType;
            Cooldown = cooldown;
            Duration = duration;
        }

        #endregion
    }
}