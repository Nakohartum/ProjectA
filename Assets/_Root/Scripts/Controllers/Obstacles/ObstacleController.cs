using System;
using System.Collections;
using _Root.Scripts.Models;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;
using DG.Tweening;
using UnityEngine;

namespace _Root.Scripts.Controllers.Obstacles
{
    public abstract class ObstacleController
    {
        #region Fields
        
        protected ObstacleView _obstacleView;
        protected IObstacleModel _obstacleModel;
        
        public event Action<float> OnPlayerCollide = f => { };

        #endregion


        #region Constructor

        protected ObstacleController(ObstacleView obstacleView, IObstacleModel obstacleModel)
        {
            _obstacleView = obstacleView;
            _obstacleModel = obstacleModel;
            _obstacleView.OnPlayerCollide += ApplyEffect;
            
        }


        #endregion
        
        
        #region MyRegion

        protected virtual void ApplyEffect()
        {
            OnPlayerCollide.Invoke(_obstacleModel.Damage);
        }

        #endregion


    }
}