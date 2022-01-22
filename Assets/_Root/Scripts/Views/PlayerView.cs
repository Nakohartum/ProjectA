using System;
using UnityEngine;


namespace _Root.Scripts.Views
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public Collider2D Collider { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public SpriteRenderer Renderer { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public ParticleSystem BloodParticles { get; private set; }
        [field: SerializeField] public ParticleSystem EvaporationParticle { get; private set; }

        private void Awake()
        {
            Collider = GetComponent<BoxCollider2D>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Renderer = GetComponentInChildren<SpriteRenderer>();
            Animator = GetComponentInChildren<Animator>();
        }
    }
}