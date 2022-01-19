namespace _Root.Scripts.Controllers
{
    public class Oxygen
    {
        #region Properties

        public float MaxOxygen { get; private set; }
        public float CurrentOxygen { get; set; }
        public bool HasOxygen { get; private set; } = true;

        #endregion


        #region Constructor

        public Oxygen(float maxOxygen)
        {
            MaxOxygen = maxOxygen;
            CurrentOxygen = MaxOxygen;
        }

        #endregion


        #region Methods

        public void RemoveOxygen(float deltaTime)
        {
            CurrentOxygen -= deltaTime;
            if (CurrentOxygen == 0)
            {
                HasOxygen = false;
            }
        }

        public void AddOxygen(float value)
        {
            if (CurrentOxygen > MaxOxygen || CurrentOxygen == MaxOxygen)
            {
                return;
            }
            CurrentOxygen += value;
            if (CurrentOxygen > MaxOxygen)
            {
                CurrentOxygen = MaxOxygen;
            }
        }

        #endregion
    }
}