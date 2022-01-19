using System;
using UnityEngine;


namespace _Root.Scripts.Models
{
    [Serializable]
    public struct PlayerInformation 
    {
        [field: SerializeField] public float Speed { get; set; }
        [field: SerializeField] public float JumpSpeed { get; set; }
        [field: SerializeField] public float HealthPoint { get; private set; }
        
        [field: SerializeField] public float OxygenPoint { get; private set; }
        [field: SerializeField] public float DashPower { get; private set; }
        [field: SerializeField] public float DashTime { get; private set; }
        [field: SerializeField] public float BlockTime { get; private set; }
    }
}