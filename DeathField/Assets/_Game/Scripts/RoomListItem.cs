using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

namespace DeathField
{
    public class RoomListItem : MonoBehaviour
    {
        [SerializeField] private Text _roomName;

        private RoomInfo _currentRoom;

        public void SetUp(RoomInfo info)
        {
            _currentRoom = info;
            _roomName.text = info.Name;
        }

        public void OnClick()
        {
            Launcher.JoinRoomAction.Invoke(_currentRoom);
        }
    }
}
