using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;


namespace _Root.Scripts.Views
{
    public class CameraView : MonoBehaviour
    {
        [field: SerializeField] public float MinXPosition { get; private set; }
        [field: SerializeField] public float MaxXPosition { get; private set; }
        [field: SerializeField] public float MinYPosition { get; private set; }
        [field: SerializeField] public float MaxYPosition { get; private set; }
        [field: SerializeField] public Transform TargetPosition { get; private set; }
        [field: SerializeField] public Image FlashingImage { get; private set; }

        private void Awake()
        {
            TargetPosition = Object.FindObjectOfType<PlayerView>().transform;
        }
    } 
}