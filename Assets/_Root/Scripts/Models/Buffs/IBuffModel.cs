namespace _Root.Scripts.Models.Buffs
{
    public interface IBuffModel
    {
        float Amount { get; }
        BuffType BuffType { get; }
    }
}
