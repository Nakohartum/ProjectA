using System;
using _Root.Scripts.Models.Buffs;
using UnityEngine;

namespace _Root.Scripts.Views
{
    public class BuffView : MonoBehaviour
    {
        [field: SerializeField] public BuffConfig BuffConfig { get; private set; }
        public event Action OnPlayerCollide = () => { };
    }
}