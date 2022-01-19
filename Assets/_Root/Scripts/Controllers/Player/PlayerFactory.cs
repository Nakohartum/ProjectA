using _Root.Configs;
using _Root.Scripts.Models;


namespace _Root.Scripts.Controllers
{
    public class PlayerFactory
    {
        #region Fields

        private readonly LevelObjects _levelObjects;
        private readonly ExecutableObjects _executableObjects;
        private IPlayerModel _playerModel;
        
        #endregion


        #region Constructor

        public PlayerFactory(LevelObjects levelObjects, ExecutableObjects executableObjects)
        {
            _levelObjects = levelObjects;
            _executableObjects = executableObjects;
        }

        #endregion



        #region Methods

        public PlayerController CreatePlayer()
        {
            var oxygen = new Oxygen(_levelObjects.PlayerInformationObject.PlayerInformation.OxygenPoint);
            var health = new Health(_levelObjects.PlayerInformationObject.PlayerInformation.HealthPoint);
            var contactPoller = new ContactPoller(_levelObjects.PlayerView.Collider);
            _playerModel = new PlayerModel(_levelObjects.PlayerInformationObject.PlayerInformation.Speed, 
                _levelObjects.PlayerInformationObject.PlayerInformation.JumpSpeed, health
                , oxygen,
                _levelObjects.PlayerInformationObject.PlayerInformation.DashPower,
                _levelObjects.PlayerInformationObject.PlayerInformation.DashTime,
                _levelObjects.PlayerInformationObject.PlayerInformation.BlockTime);
            var playerInputController =
                new PlayerInputController(_playerModel, _levelObjects.PlayerView.Rigidbody2D, contactPoller, 
                    _levelObjects.PlayerView);
            var playerController = new PlayerController(_levelObjects.PlayerView, _playerModel, 
                playerInputController, _executableObjects);
            return playerController;
        }

        public IPlayerModel GetPlayerModel()
        {
            return _playerModel;
        }
        
        #endregion
    }
}