using System.Collections.Generic;
using _Root.Configs;
using _Root.Scripts.Controllers;
using _Root.Scripts.Controllers.Camera;
using _Root.Scripts.Controllers.Obstacles;
using _Root.Scripts.Models;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;

namespace _Root.Scripts
{
    public class GameInitializer
    {
        public GameInitializer(ExecutableObjects executableObjects, LevelObjects levelObjects, List<ObstacleView> obstacleViews)
        {
            var obstacleFactory = new ObstacleFactory(obstacleViews);
            var playerFactory = new PlayerFactory(levelObjects, executableObjects);
            var playerController = playerFactory.CreatePlayer();
            var obstacleControllers = obstacleFactory.CreateObstacles();
            var cameraContoller = new CameraController(levelObjects.CameraView, executableObjects);
            executableObjects.AddExecutable(playerController);
            executableObjects.AddExecutable(cameraContoller);
            for (int i = 0; i < obstacleControllers.Count; i++)
            {
                obstacleControllers[i].OnPlayerCollide += playerFactory.GetPlayerModel().Health.RemoveHealthPoints;
            }
        }
    }
}