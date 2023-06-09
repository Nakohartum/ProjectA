﻿using UnityEngine;


namespace _Root.Scripts.Models.Obstacles
{
    [CreateAssetMenu(fileName = nameof(ObstacleConfig), menuName = "Models/"+nameof(ObstacleConfig), order = 0)]
    public class ObstacleConfig : ScriptableObject
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float Distance { get; private set; }
        [field: SerializeField] public ObstacleType ObstacleType { get; private set; }
        [field: SerializeField] public DamageType DamageType { get; private set; }
    }

    public enum ObstacleType
    {
        Pike            = 0,
        Spike           = 1,
        Saw             = 2,
        Flamethrower    = 3
    }
    
    public enum DamageType
    {
        Health  = 0,
        Oxygen  = 1
    }
}