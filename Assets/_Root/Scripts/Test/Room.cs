using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Root.Scripts.Test
{
    public class Room : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text _maxPlayers;
        [SerializeField] private TMP_Text _playersCount;
        [SerializeField] private RectTransform _playersAddPlace;
        [SerializeField] private PlayerInRoom _playerInRoomPrefab;
        [SerializeField] private float _offset;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Toggle _openClosedToggle;
        private int _currentPlayersCount = 0;
        private List<PlayerInRoom> _playersInRoom = new List<PlayerInRoom>();
        private Canvas _canvas;

        public override void OnEnable()
        {
            base.OnEnable();
            _startGameButton.onClick.AddListener(StartGame);
            _exitButton.onClick.AddListener(ExitRoom);
            _openClosedToggle.onValueChanged.AddListener(ChangeRoomOpenClosedPolicy);
        }

        

        public override void OnDisable()
        {
            base.OnDisable();
            _startGameButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _openClosedToggle.onValueChanged.RemoveAllListeners();
        }

        private void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void StartGame()
        {
            PhotonNetwork.LoadLevel("GameScene");
        }

        private void ChangeRoomOpenClosedPolicy(bool value)
        {
            PhotonNetwork.NetworkingClient.CurrentRoom.IsOpen = value;
        }


        private void ExitRoom()
        {
            PhotonNetwork.NetworkingClient.OpLeaveRoom(false);
        }

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void Initialize(string maxPlayers)
        {
            _maxPlayers.text = $"/{maxPlayers}";
            _canvas.enabled = true;
        }

        public void AddPlayer(string nickName, string userID)
        {
            _currentPlayersCount++;
            _playersCount.text = $"{_currentPlayersCount.ToString()}";
            var player = Instantiate(_playerInRoomPrefab, _playersAddPlace);
            player.SetID(userID);
            player.SetNickName(nickName);
            if (_playersInRoom.Count == 0)
            {
                _playersInRoom.Add(player);
            }
            else
            {
                var element = _playersInRoom.Last();
                Vector3 positionOfLast = element.transform.localPosition;
                Vector3 newElement = new Vector3
                {
                    x = positionOfLast.x,
                    y = positionOfLast.y - element.GetComponent<RectTransform>().rect.height - _offset,
                    z = positionOfLast.z
                };
                player.transform.localPosition = newElement;

                float contentHeight = _playersAddPlace.rect.height;

                contentHeight += _offset + element.GetComponent<RectTransform>().rect.height;
                
                _playersAddPlace.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentHeight);
                _playersInRoom.Add(player);
            }
        }

        private void RemovePlayer(PlayerInRoom playerInRoom)
        {
            _currentPlayersCount--;
            _playersCount.text = $"{_currentPlayersCount.ToString()}";
            var element = _playersInRoom.Last();
            Vector3 positionOfLast = element.transform.localPosition;
            Vector3 newElement = new Vector3
            {
                x = positionOfLast.x,
                y = positionOfLast.y + element.GetComponent<RectTransform>().rect.height + _offset,
                z = positionOfLast.z
            };
            float contentHeight = _playersAddPlace.rect.height;
            contentHeight += _offset - element.GetComponent<RectTransform>().rect.height;
            _playersAddPlace.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentHeight); 
            _playersInRoom.Remove(playerInRoom);
            Destroy(playerInRoom.gameObject);
            }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            UpdateUsersInRoom();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            UpdateUsersInRoom();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            RemovePlayer(_playersInRoom.Find(x => x.GetNickName() == otherPlayer.NickName));
        }

        private void UpdateUsersInRoom()
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PublishUserId);
            Initialize(PhotonNetwork.CurrentRoom.MaxPlayers.ToString());
            foreach (var player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                if (_playersInRoom.FindAll(x => x.GetNickName() == player.NickName).Count > 0)
                {
                    continue;
                }
                AddPlayer(player.NickName, player.UserId.Substring(0, 4));
            }
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            _canvas.enabled = false;
        }
    }
}