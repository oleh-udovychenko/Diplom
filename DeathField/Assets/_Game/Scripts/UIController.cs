using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeathField
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instanse { get; private set; }

        [SerializeField] private Launcher _launcher;

        [SerializeField] private GameObject _titleMenu;
        [SerializeField] private GameObject _loading;
        [SerializeField] private GameObject _createRoom;
        [SerializeField] private GameObject _room;
        [SerializeField] private GameObject _findRoom;

        [SerializeField] private InputField _roomNameInput;

        [SerializeField] private Text _roomNameText;

        [SerializeField] private RectTransform _roonListField;
        [SerializeField] private RoomListItem _roomListItemPrefab;

        private void Awake()
        {
            if (Instanse == null)
                Instanse = this;
        }

        public void OpenPage(Pages page)
        {
            switch (page)
            {
                case Pages.Title:
                    _titleMenu.SetActive(true);

                    _loading.SetActive(false);
                    _createRoom.SetActive(false);
                    _room.SetActive(false);
                    _findRoom.SetActive(false);
                    break;

                case Pages.Loading:
                    _loading.SetActive(true);

                    _titleMenu.SetActive(false);
                    _createRoom.SetActive(false);
                    _room.SetActive(false);
                    _findRoom.SetActive(false);
                    break;

                case Pages.CreateRoom:
                    _createRoom.SetActive(true);

                    _loading.SetActive(false);
                    _titleMenu.SetActive(false);
                    _room.SetActive(false);
                    _findRoom.SetActive(false);
                    break;

                case Pages.Room:
                    _room.SetActive(true);

                    _loading.SetActive(false);
                    _titleMenu.SetActive(false);
                    _createRoom.SetActive(false);
                    _findRoom.SetActive(false);

                    _roomNameText.text = _launcher.GetRoomName();
                    break;
            }
        }

        public void CreateRoom()
        {
            if (!string.IsNullOrEmpty(_roomNameInput.text))
            {
                _launcher.CreateRoom(_roomNameInput.text);

                OpenPage(Pages.Loading);
            }
        }

        public void StartGame()
        {
            _launcher.StartGame();
        }

        public void LiaveRoom()
        {
            _launcher.LiaveRoom();
        }

        public void UpdateRoomItems(List<RoomInfo> roomList)
        {
            var items = _roonListField.GetComponentsInChildren<RoomListItem>();
            foreach (var itemPrefab in items)
                Destroy(itemPrefab.gameObject);

            foreach (var room in roomList)
            {
                var item = Instantiate(_roomListItemPrefab, _roonListField);
                item.SetUp(room);
            }
        }

        public enum Pages
        {
            Title,
            Loading,
            CreateRoom,
            Room,
        }
    }
}
