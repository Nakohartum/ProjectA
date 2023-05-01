using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Root.Scripts.Test
{
    public class PhotonTest : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Button _connectToPhotonServer;
        [SerializeField] private Button _disconnectFromPhotonServer;
        [SerializeField] private TextMeshProUGUI _photonLabel;
        private string _gameVersion = "1";

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            ChangeInteractionState();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _connectToPhotonServer.onClick.AddListener(Connect);
            _disconnectFromPhotonServer.onClick.AddListener(Disconnect);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _connectToPhotonServer.onClick.RemoveAllListeners();
            _disconnectFromPhotonServer.onClick.RemoveAllListeners();
        }

        private void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = _gameVersion;
            }

            ChangeInteractionState();
        }
        
        private void Disconnect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Disconnect();
            }
            
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            _photonLabel.text = "Connected to Photon";
            _photonLabel.color = Color.green;
            Debug.Log("OnConnectedToMaster() was called by PUN");
            ChangeInteractionState();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            _photonLabel.text = "Disconnected from Photon";
            _photonLabel.color = Color.red;
            Debug.Log(cause);
        }

        private void ChangeInteractionState()
        {
            _connectToPhotonServer.interactable = !PhotonNetwork.IsConnected;
            _disconnectFromPhotonServer.interactable = PhotonNetwork.IsConnected;
        }
    }
}