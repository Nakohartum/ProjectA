namespace _Root.Scripts.Models.Buffs
{
    public class BuffModel : IBuffModel
    {
        public float Amount { get; }
        public BuffType BuffType { get; }

        public BuffModel(float amount, BuffType buffType)
        {
            Amount = amount;
            BuffType = buffType;
        }
    }
}