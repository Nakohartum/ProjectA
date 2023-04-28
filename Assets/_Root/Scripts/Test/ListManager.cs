using System.Collections.Generic;
using System.Linq;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Root.Scripts.Test
{
    public class ListManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private RoomInLobby roomInLobbyPrefab;
        [SerializeField] private float _offset;
        private List<RoomInLobby> _roomsList = new List<RoomInLobby>();
        

        public void AddRoom(RoomInfo roomInfo)
        {
            var go = Instantiate(roomInLobbyPrefab, _content);
            go.SetRoomName(roomInfo.Name);
            go.SetMaxPlayers(roomInfo.MaxPlayers.ToString());
            if (_roomsList.Count == 0)
            {
                _roomsList.Add(go);
            }
            else
            {
                var element = _roomsList.Last();
                Vector3 positionOfLast = element.transform.localPosition;
                Vector3 newElement = new Vector3
                {
                    x = positionOfLast.x,
                    y = positionOfLast.y - element.GetComponent<RectTransform>().rect.height - _offset,
                    z = positionOfLast.z
                };
                go.transform.localPosition = newElement;

                float contentHeight = _content.rect.height;

                contentHeight += _offset + element.GetComponent<RectTransform>().rect.height;
                
                _content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentHeight);
                go.SetMaxPlayers(roomInfo.MaxPlayers.ToString());
                go.SetRoomName(roomInfo.Name);
                _roomsList.Add(go);
            }
        }

        public void RemoveAllRooms()
        {
            for (int i = 0; i < _roomsList.Count; i++)
            {
                var element = _roomsList[i];

                float contentHeight = _content.rect.height;

                contentHeight -= _offset - element.GetComponent<RectTransform>().rect.height;
                
                _content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentHeight);
                Destroy(element.gameObject);
            }
            _roomsList.Clear();
        }
    }
}