using UnityEngine;


namespace _Root.Scripts.Views
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public Collider2D Collider { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public SpriteRenderer Renderer { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }

        
    }
}