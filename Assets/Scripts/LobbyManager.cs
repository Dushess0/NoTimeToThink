using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
  
    
    private string GameVersion;


    [SerializeField]
    private InputField password;
    [SerializeField]
    private InputField roomName;

    [SerializeField]
    private Button quickbutton;

    [SerializeField]
    private Button createroom;

    [SerializeField]
    private Button joinroom;




    private List<RoomInfo> rooms;
    private bool is_quickmatch = false;


    private void Start()
    {

        joinroom.interactable = false;
        createroom.interactable = false;
        quickbutton.interactable = false;


      
        PhotonNetwork.NickName = "Player#" + Random.Range(0, 100);
      

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.95";
        PhotonNetwork.ConnectUsingSettings();





        rooms = new List<RoomInfo>();
    }
    public override void OnConnectedToMaster()
    {
        joinroom.interactable = true;
        createroom.interactable = true;
        quickbutton.interactable = true;


        joinroom.onClick.AddListener(JoinPrivateRoom);
        createroom.onClick.AddListener(CreatePrivateRoom);
        quickbutton.onClick.AddListener(QuickMatch);




    }
   
    public  void CreatePrivateRoom()
    {
        PhotonNetwork.CreateRoom(roomName.text+password.text, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }
    public  void JoinPrivateRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text + password.text);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {

        roomName.text = "Error";
        password.text = "Error";
        if (is_quickmatch)
        {
            PhotonNetwork.CreateRoom("test", new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
        }


    }
    public  void QuickMatch()
    {
        is_quickmatch = true;
        if (!PhotonNetwork.JoinRoom("test"))
        {
            Debug.Log("joined");
        }
      

        
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        this.rooms = roomList;


    }


    public override void OnJoinedRoom()
    {
        
        PhotonNetwork.LoadLevel("Game");

    }


 

}
