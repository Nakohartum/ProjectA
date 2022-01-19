using System;


namespace _Root.Scripts.Controllers.Interfaces
{
    public interface IInput
    {
        event Action<float> OnAxisChange;
        
    }
}