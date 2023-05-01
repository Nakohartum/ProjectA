using System;
using Photon.Pun;
using UnityEngine;

namespace _Root.Scripts.Test
{
    public class GameRoomManager : MonoBehaviourPunCallbacks
    {
        public static GameRoomManager Instance;
        [SerializeField] private GameObject _playerObject;

        private void Start()
        {
            Instance = this;
            if (PlayerController.LocalPlayerObject == null)
            {
                PlayerController.LocalPlayerObject = PhotonNetwork.Instantiate(_playerObject.name, new Vector3(0f, 5f, 0f), Quaternion.identity);
            }
        }
        
        
    }
}