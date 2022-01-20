using System.Collections.Generic;
using System.Linq;
using _Root.Configs;
using _Root.Scripts;
using _Root.Scripts.Controllers;
using _Root.Scripts.Models;
using _Root.Scripts.Views;
using UnityEngine;


public class EntryPoint : MonoBehaviour
{
    #region Fields

    [SerializeField] private PlayerView _playerView;
    [SerializeField] private CameraView _cameraView;
    [SerializeField] private PlayerInformationObject _playerInformationObject;
    [SerializeField] private List<ObstacleView> _obstacleViews;
    [SerializeField] private List<PortalView> _portalViews;
    private ExecutableObjects _executableObjects;

    #endregion


    #region Unity Methods

    private void Awake()
    {
        _portalViews = Object.FindObjectsOfType<PortalView>().ToList();
        _obstacleViews = Object.FindObjectsOfType<ObstacleView>().ToList();
        _playerView = Object.FindObjectOfType<PlayerView>();
        _cameraView = Object.FindObjectOfType<CameraView>();
        _executableObjects = new ExecutableObjects();
        var levelObjects = new LevelObjects();
        levelObjects.CameraView = _cameraView;
        levelObjects.PlayerView = _playerView;
        levelObjects.PlayerInformationObject = _playerInformationObject;
        new GameInitializer(_executableObjects, levelObjects, _obstacleViews, _portalViews);
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        _executableObjects.Execute(deltaTime);
    }
    
    

    #endregion
}
