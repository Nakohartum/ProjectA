using System;
using System.Collections.Generic;
using _Root.Configs;
using _Root.Scripts.Controllers;
using _Root.Scripts.Controllers.Camera;
using _Root.Scripts.Controllers.Level;
using _Root.Scripts.Controllers.Obstacles;
using _Root.Scripts.Models.Obstacles;
using _Root.Scripts.Views;


namespace _Root.Scripts
{
    public class GameInitializer
    {
        #region Constructor

        public GameInitializer(ExecutableObjects executableObjects, LevelObjects levelObjects, List<ObstacleView> obstacleViews,
            List<PortalView> portalViews, List<StickyFloor> stickyFloors, List<BuffView> buffViews)
        {
            var obstacleFactory = new ObstacleFactory(obstacleViews, executableObjects);
            var playerFactory = new PlayerFactory(levelObjects, executableObjects);
            var buffFactory = new BuffFactory(buffViews, executableObjects);
            var playerController = playerFactory.CreatePlayer();
            var obstacleControllers = obstacleFactory.CreateObstacles();
            var cameraContoller = new CameraController(levelObjects.CameraView, executableObjects, playerController);
            var buffControlelrs = buffFactory.CreateBuffs();
            executableObjects.AddExecutable(playerController);
            executableObjects.AddExecutable(cameraContoller);
            for (int i = 0; i < obstacleControllers.Count; i++)
            {
                obstacleControllers[i].OnPlayerCollide.AddListener(playerController.ApplyNegativeEffects);
            }
            // for (int i = 0; i < buffControlelrs.Count; i++)
            // {
            //     switch (obstacleViews[i].ObstacleConfig.ObstacleType)
            //     {
            //         case ObstacleType.Pike:
            //             obstacleControllers[i].OnPlayerCollide.AddListener(playerController.ApplyNegativeEffects);
            //             break;
            //         case ObstacleType.Spike:
            //             obstacleControllers[i].OnPlayerCollide.AddListener(playerController.ApplyNegativeEffects);
            //             break;
            //         case ObstacleType.Saw:
            //             obstacleControllers[i].OnPlayerCollide.AddListener(playerController.ApplyNegativeEffects);
            //             break;
            //         case ObstacleType.Flamethrower:
            //             obstacleControllers[i].OnPlayerCollide.AddListener(playerController.ApplyNegativeEffects);
            //             break;
            //     }
            // }

            for (int i = 0; i < portalViews.Count; i++)
            {
                new PortalController(portalViews[i], playerController);
            }

            for (int i = 0; i < stickyFloors.Count; i++)
            {
                stickyFloors[i].ConnectedCollider.OnCollide += playerController.BlockJump;
            }
            
            
        }

        #endregion
        
    }
}