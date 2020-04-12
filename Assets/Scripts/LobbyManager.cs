using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text LogText;

    [SerializeField]
    private string GameVersion;

    private void Start()
    {
        PhotonNetwork.NickName = "Player#" + Random.Range(0, 100);
        Log("Username=" + PhotonNetwork.NickName);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.01";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Log("Connected to Master");
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });

    }
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();


    }

    public override void OnJoinedRoom()
    {
        Log("Joined Room");
        PhotonNetwork.LoadLevel("Game");

    }


    private void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }

}
