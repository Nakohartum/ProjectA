using System;
using _Root.Scripts.Models;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace _Root.Scripts.Views
{
    public class ObstacleView : MonoBehaviour
    {
        public ObstacleConfig ObstacleConfig;
        public event Action OnPlayerCollide = () => { };
        public event Action OnStart = () => { };
        public ConnectedCollider ConnectedCollider;
        public ParticleSystem Particles;

        private void Start()
        {
            OnStart.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnPlayerCollide.Invoke();
            Debug.Log("Done");
        }
    }
}