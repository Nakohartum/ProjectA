using UnityEngine;

namespace _Root.Scripts.Models.Buffs
{
    [CreateAssetMenu(fileName = nameof(BuffConfig), menuName = "Configs/"+nameof(BuffConfig), order = 0)]
    public class BuffConfig : ScriptableObject
    {
        [field: SerializeField] public float Amount { get; private set; }
        [field: SerializeField] public BuffType BuffType { get; private set; }
    }

    public enum BuffType
    {
        Heal    = 1,
        Oxygen  = 2
    }
}