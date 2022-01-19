using System;
using _Root.Scripts.Controllers.Interfaces;
using UnityEngine;


namespace _Root.Scripts.Controllers
{
    public class PlayerHorizontalInput : IInput, IExecutable
    {
        #region Fields

        public event Action<float> OnAxisChange = f => { };

        #endregion


        #region Constructor

        public PlayerHorizontalInput()
        {
        }

        #endregion


        #region Methods

        public void Execute(float deltaTime)
        {
            OnAxisChange.Invoke(Input.GetAxis("Horizontal"));
        }

        #endregion
        

        
    }
}