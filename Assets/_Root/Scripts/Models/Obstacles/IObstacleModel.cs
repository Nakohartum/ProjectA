namespace _Root.Scripts.Models.Obstacles
{
    public interface IObstacleModel
    {
        float Damage { get; }
        float Cooldown { get; }
        ObstacleType ObstacleType { get; }
    }
}