using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

namespace Chess
{

    public class PlayerController : MonoBehaviourPun, Photon.Realtime.IPunObservable, Photon.Pun.IPunObservable
    {
        [SerializeField]
        private Camera cam;
        public ChessColor color { get; private set; }

        public ChessGUI GUI;

        private ChessManager manager;


       public void Init(ChessColor chessColor,ChessManager man)
        {
            color = chessColor;
            manager = man;
        }

        private void Start()
        {
            
            if (photonView.IsMine)
            {
                EnableCamera();
                GUI = FindObjectOfType<ChessGUI>();
                GUI.player = this;
            }
           
        }
   
        public void EnableCamera()
        {
            cam.gameObject.SetActive(true);
            
        }
        private void Update()
        {
            
            
            if (!photonView.IsMine) return;


            if (Input.GetMouseButtonDown(0))
            {
                var ray=  cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray,out hit))
                {
                    var tile = hit.transform.GetComponent<Tile>();
                    if (tile)
                    {
                     
                        if (GUI.ProcessTile(tile))
                            manager.PlayerWantMove(GUI.lastSelected.index, tile.index);
                    }
                    //todo check colllisions with figures

                    

                }
                
            }
        }
      
     
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

            //if (allTiles == null) GetTiles();

            //if (stream.IsWriting)
            //{
            //    //send index
            //    if (selectedTile)
            //        stream.SendNext(selectedTile.y * 8 + selectedTile.x);
            //    else
            //        stream.SendNext(404);
            //    byte[] highlighted = new byte[64];
            //    int i = 0;
            //    foreach (var item in this.highlighted)
            //    {
            //        highlighted[i] = (byte)(item.y * 8 + item.x);
            //        i++;
            //    }
            //    stream.SendNext(highlighted);
            //}
            //else
            //{
            //    var selected = (int)stream.ReceiveNext();
            //    if (selected != 404)
            //    {
            //        this.selectedTile = allTiles[selected];
            //    }
            //    byte[] highlighted = (byte[])stream.ReceiveNext();
            //    this.highlighted = new List<Tile>();
            //    foreach (var item in highlighted)
            //    {
            //        if (item != 0)
            //            this.highlighted.Add(allTiles[item]);
            //    }

            //}
            
          //  Debug.LogFormat("Player {0} selected tile {1}", photonView.Owner.NickName,selectedTile.y*8+selectedTile.x);
        }

        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code==15) //move figure from tile A to selected tile
            {

            }
        }
    }
}