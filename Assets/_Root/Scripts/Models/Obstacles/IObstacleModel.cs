namespace _Root.Scripts.Models.Obstacles
{
    public interface IObstacleModel
    {
        float Damage { get; }
        float Cooldown { get; }
        float Duration { get; }
        ObstacleType ObstacleType { get; }
    }
}