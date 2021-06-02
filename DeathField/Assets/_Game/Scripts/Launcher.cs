using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace DeathField
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public static Action<RoomInfo> JoinRoomAction;

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
            JoinRoomAction += JoinRoom;
        }

        private void OnDestroy()
        {
            JoinRoomAction -= JoinRoom;
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            Debug.Log("Connected to Master");
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();

            Debug.Log("Join Lobby");


        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            Debug.Log("Join Room");
            UIController.Instanse.OpenPage(UIController.Pages.Room);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);

            Debug.Log("Create Room Failed With Code " + returnCode);
            Debug.Log(message);
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            UIController.Instanse.OpenPage(UIController.Pages.Title);
            Debug.Log("Leave Room");
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);

            UIController.Instanse.UpdateRoomItems(roomList);
        }

        public void CreateRoom(string name)
        {
            PhotonNetwork.CreateRoom(name);
            Debug.Log("Create Room");
        }

        public void LiaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            UIController.Instanse.OpenPage(UIController.Pages.Loading);
        }

        public void JoinRoom(RoomInfo info)
        {
            UIController.Instanse.OpenPage(UIController.Pages.Loading);
            PhotonNetwork.JoinRoom(info.Name);
        }

        public void StartGame()
        {
            PhotonNetwork.LoadLevel(1);
        }

        public string GetRoomName()
        {
            return PhotonNetwork.CurrentRoom.Name;
        }
    }
}
