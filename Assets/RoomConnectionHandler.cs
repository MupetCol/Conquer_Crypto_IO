using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomConnectionHandler : MonoBehaviourPunCallbacks
{
    public string _roomName;
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom("AAA", roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("AAA");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Map_1");
    }
    
}
