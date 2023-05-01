using Photon.Pun;
using TMPro;
using UnityEngine;

namespace _Root.Scripts.Test
{
    public class PlayerInRoom : MonoBehaviour
    {
        [SerializeField] private TMP_Text _id;
        [SerializeField] private TMP_Text _nickName;

        public void SetID(string id)
        {
            _id.text = id;
        }
        
        public void SetNickName(string nickName)
        {
            _nickName.text = nickName;
        }

        public string GetNickName()
        {
            return _nickName.text;
        }

        public void EnterTheRoom()
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }
}