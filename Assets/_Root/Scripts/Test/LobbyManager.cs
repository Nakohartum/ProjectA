using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace _Root.Scripts.Test
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private ServerSettings _serverSettings;
        [SerializeField] private ListManager _listManager;
        [SerializeField] private Button _createRoomButton;
        [SerializeField] private Room _room;
        [SerializeField] private Canvas _lobbyObject;
        private EnterRoomParams _enterRoomParams;
        
        public override void OnEnable()
        {
            base.OnEnable();
            _createRoomButton.onClick.AddListener(CreateRoom);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _createRoomButton.onClick.RemoveAllListeners();
        }

        private void CreateRoom()
        {
            _enterRoomParams = new EnterRoomParams()
            {
                RoomName = "Example1",
                RoomOptions = new RoomOptions()
                {
                    MaxPlayers = 4,
                    PublishUserId = true
                }
            };
            PhotonNetwork.NetworkingClient.OpCreateRoom(_enterRoomParams);
        }

        private void Start()
        {
            PhotonNetwork.NetworkingClient.ConnectUsingSettings(_serverSettings.AppSettings);
            _createRoomButton.interactable = false;
        }

        public override void OnConnected()
        {
            base.OnConnected();
            Debug.Log("OnConnected");
            
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            Debug.Log("OnJoinedLobby");
            _createRoomButton.interactable = true;
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            Debug.Log("OnConnectedToMaster");
            PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), result =>
            {
                PhotonNetwork.NickName = result.AccountInfo.Username;
                PhotonNetwork.NetworkingClient.OpJoinLobby(new TypedLobby("Main", LobbyType.Default));
            }, error =>
            {
                Debug.Log($"{error.ErrorMessage}");
            } );
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            Debug.Log($"Join failed: {message}");
            
            PhotonNetwork.NetworkingClient.OpCreateRoom(new EnterRoomParams()
            {
                RoomName = "Example",
                RoomOptions = new RoomOptions()
                {
                    PublishUserId = true
                }
            });
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            _lobbyObject.enabled = false;
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
            _listManager.RemoveAllRooms();
            if (roomList.Count != 0)
            {
                for (int i = 0; i < roomList.Count; i++)
                {
                    _listManager.AddRoom(roomList[i]);
                }
            }
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            _lobbyObject.enabled = true;
        }
        
    }
}