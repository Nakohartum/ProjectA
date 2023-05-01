using System;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace _Root.Scripts.Test
{
    public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
    {
        public static GameObject LocalPlayerObject;
        [SerializeField] private float _health;
        [SerializeField] private float _speed;

        private void Start()
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnUserDataGot, error =>
            {
                Debug.Log(error.ErrorMessage);
            });
        }

        private void OnUserDataGot(GetUserDataResult obj)
        {
            if (obj.Data.TryGetValue("HealthPoints", out var health))
            {
                Debug.Log(health.Value);
                float.TryParse(health.Value, out var hp);
                _health = hp;
            }
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
        }

        private void Update()
        {
            if (!photonView.IsMine)
            {
                return;
            }
            photonView.transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _speed, Space.World);
            _health -= Time.deltaTime;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_health);
                stream.SendNext(_speed);
            }
            else
            {
                _health = (float) stream.ReceiveNext();
                _speed = (float) stream.ReceiveNext();
            }
        }
    }
}