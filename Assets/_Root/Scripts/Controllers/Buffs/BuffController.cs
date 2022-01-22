using System;
using _Root.Scripts.Controllers.Interfaces;
using _Root.Scripts.Models.Buffs;
using _Root.Scripts.Views;
using UnityEngine.Events;
using Object = UnityEngine.Object;


namespace _Root.Scripts.Controllers.Obstacles
{
    public abstract class BuffController : IExecutable, IDisposable
    {
        #region Fields
        
        protected readonly BuffView _buffView;
        protected readonly IBuffModel _buffModel;

        public readonly UnityEvent<float, BuffType> OnPlayerCollide = new UnityEvent<float, BuffType>();

        #endregion


        #region Constructor

        protected BuffController(BuffView buffView, IBuffModel buffModel)
        {
            _buffView = buffView;
            _buffModel = buffModel;
            _buffView.OnPlayerCollide += ApplyEffect;
            _buffView.OnObjectDestroy += Dispose;
        }


        #endregion
        
        
        #region MyRegion

        protected virtual void ApplyEffect()
        {
            OnPlayerCollide.Invoke(_buffModel.Amount, _buffModel.BuffType);
            Object.Destroy(_buffView.gameObject);
        }
        
        public virtual void Execute(float deltaTime)
        {
            
        }

        
        
        #endregion


        public void Dispose()
        {
            _buffView.OnPlayerCollide -= ApplyEffect;
            _buffView.OnObjectDestroy -= Dispose;
        }
    }
}