using System;
using System.Collections.Generic;
using _Root.Scripts.Models.Buffs;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;


namespace _Root.Scripts.Controllers.Obstacles
{
    public class BuffFactory
    {
        #region Fields

        private readonly List<BuffView> _buffViews;
        private readonly ExecutableObjects _executableObjects;

        #endregion


        #region Constructor

        public BuffFactory(List<BuffView> buffViews, ExecutableObjects executableObjects)
        {
            _buffViews = buffViews;
            _executableObjects = executableObjects;
        }

        #endregion


        #region Methods

        public List<BuffController> CreateBuffs()
        {
            List<BuffController> buffControllers = new List<BuffController>();
            for (int i = 0; i < _buffViews.Count; i++)
            {
                var model = new BuffModel(_buffViews[i].BuffConfig.Amount, _buffViews[i].BuffConfig.BuffType);
                BuffController buffController = new NullBuffController(_buffViews[i], model);
                switch (_buffViews[i].BuffConfig.BuffType)
                {
                    case BuffType.Heal:
                        buffController = new HealController(_buffViews[i], model);
                        break;
                    case BuffType.Oxygen:
                        buffController = new OxygenController(_buffViews[i], model);
                        break;
                }
                
                buffControllers.Add(buffController);
            }

            return buffControllers;
        }

        #endregion
        
    }
}