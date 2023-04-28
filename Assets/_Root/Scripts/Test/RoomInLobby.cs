using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Root.Scripts.Test
{
    public class RoomInLobby : MonoBehaviour
    {
        [SerializeField] private TMP_Text _roomName;
        [SerializeField] private TMP_Text _playerCount;
        [SerializeField] private Button _connectButton;

        private void OnEnable()
        {
            _connectButton.onClick.AddListener(ConnectToRoom);
        }

        private void ConnectToRoom()
        {
            PhotonNetwork.NetworkingClient.OpJoinRoom(new EnterRoomParams()
            {
                RoomName = _roomName.text
            });
        }

        public void SetRoomName(string name)
        {
            _roomName.SetText(name);
        }
        
        public void SetMaxPlayers(string playersCount)
        {
            _playerCount.SetText($"{playersCount}");
        }

    }
}