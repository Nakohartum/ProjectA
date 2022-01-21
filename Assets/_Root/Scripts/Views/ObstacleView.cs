using System;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Utilities;
using UnityEngine;


namespace _Root.Scripts.Views
{
    public class ObstacleView : MonoBehaviour
    {
        #region Properties

        public ObstacleConfig ObstacleConfig;
        public event Action OnPlayerCollide = () => { };
        public event Action OnStart = () => { };
        public ConnectedCollider ConnectedCollider;
        public ParticleSystem Particles;
        public Animator Animator;
        public event Action OnAnimationEnds = () => { };

        #endregion


        #region Unity Methods

        private void Awake()
        {
            ConnectedCollider = GetComponentInChildren<ConnectedCollider>();
        }

        private void Start()
        {
            OnStart.Invoke();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            OnPlayerCollide.Invoke();
        }

        public void EndAnimation()
        {
            OnAnimationEnds.Invoke();
        }

        #endregion
    }
}