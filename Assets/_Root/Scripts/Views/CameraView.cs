using UnityEngine;


namespace _Root.Scripts.Views
{
    public class CameraView : MonoBehaviour
    {
        [field: SerializeField] public float MinXPosition { get; private set; }
        [field: SerializeField] public float MaxXPosition { get; private set; }
        [field: SerializeField] public float MinYPosition { get; private set; }
        [field: SerializeField] public float MaxYPosition { get; private set; }
        [field: SerializeField] public Transform TargetPosition { get; private set; }
    } 
}