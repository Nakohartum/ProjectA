using System;
using _Root.Scripts.Controllers.Interfaces;
using UnityEngine;


namespace _Root.Scripts.Controllers
{
    public class PlayerJumpController : IInput, IExecutable
    {
        #region Fields

        public event Action<float> OnAxisChange = f => { };

        #endregion



        #region Constructor

        public PlayerJumpController()
        {
        }

        #endregion



        #region Methods

        public void Execute(float deltaTime)
        {
            if (Input.GetButton("Jump"))
            {
                OnAxisChange.Invoke(1);
            }
            else
            {
                OnAxisChange.Invoke(0);
            }
        }

        #endregion

        
    }
}