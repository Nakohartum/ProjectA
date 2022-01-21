using _Root.Scripts.Controllers.Interfaces;
using _Root.Scripts.Models.Buffs;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;
using UnityEngine.Events;


namespace _Root.Scripts.Controllers.Obstacles
{
    public abstract class BuffController : IExecutable
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
        }


        #endregion
        
        
        #region MyRegion

        protected virtual void ApplyEffect()
        {
            OnPlayerCollide.Invoke(_buffModel.Amount, _buffModel.BuffType);
        }
        
        public virtual void Execute(float deltaTime)
        {
            
        }

        #endregion


        
    }
}