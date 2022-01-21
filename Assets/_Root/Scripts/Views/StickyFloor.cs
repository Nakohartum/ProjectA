using System;
using _Root.Scripts.Utilities;
using UnityEngine;


namespace _Root.Scripts.Views
{
    public class StickyFloor : MonoBehaviour
    {
        [field: SerializeField] public ConnectedCollider ConnectedCollider { get; private set; }
    } 
}