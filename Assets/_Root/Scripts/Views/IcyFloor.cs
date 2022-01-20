using System;
using _Root.Scripts.Utilities;
using UnityEngine;

namespace _Root.Scripts.Views
{
    public class IcyFloor : MonoBehaviour
    {
        [field: SerializeField] public float SpeedMultiplier { get; private set; }
        [field: SerializeField] public ConnectedCollider ConnectedCollider { get; private set; }
    }
}