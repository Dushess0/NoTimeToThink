using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Chess;
using ExitGames.Client.Photon;

public class SessionManager : MonoBehaviourPunCallbacks, Photon.Realtime.IPunObservable
{
    [SerializeField]
    private ChessManager chessManager;

    [SerializeField]
    private Transform Player1Pos;
    [SerializeField]
    private Transform Player2Pos;

    


    // Update is called once per frame
    private void Start()
    {
        GameObject player;
        if (PhotonNetwork.IsMasterClient)
        {
            player = PhotonNetwork.Instantiate("Player", Player1Pos.position, Player1Pos.rotation);
            player.GetComponent<PlayerController>().EnableCamera();
        }
        else
        {
            player = PhotonNetwork.Instantiate("Player", Player2Pos.position, Player2Pos.rotation);
            player.GetComponent<PlayerController>().EnableCamera();
            RaiseEventOptions options = new RaiseEventOptions() { Receivers = ReceiverGroup.MasterClient };
            SendOptions send = new SendOptions() { Reliability = true };
            PhotonNetwork.RaiseEvent(1, 0, options, send);
        }
       
       
    }
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
      
      

        
    }
    public void StartGame()
    {
        Debug.Log("Game started");
        chessManager.gameObject.SetActive(true);
        chessManager.StartGame();

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code==1)
        { StartGame(); }
    }
}
