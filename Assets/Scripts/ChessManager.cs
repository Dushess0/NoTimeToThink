using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Linq;
namespace Chess
{
    public enum GameState
    {
        WaitingForOpponent,
        Playing,
        End

    }
    public class ChessManager : MonoBehaviourPunCallbacks, Photon.Realtime.IPunObservable
    {


        public GameState currentState;
        PlayerController controller;
       
        List<Figure> whiteFigures;
        List<Figure> blackFigures;

        private ChessColor myColor;


        [SerializeField]
        List<Tile> tiles;


        [SerializeField]
        GameObject PawnPrefab;
        [SerializeField]
        GameObject RookPrefab;
        [SerializeField]
        GameObject KnightPrefab;
        [SerializeField]
        GameObject BishopPrefab;
        [SerializeField]
        GameObject QueenPrefab;
        [SerializeField]
        GameObject KingPrefab;


        [SerializeField]
        GameObject win_label;
        [SerializeField]
        GameObject lose_label;




        public void StartGame(GameObject controller)
        {
            this.controller = controller.GetComponent<PlayerController>();
            currentState = GameState.Playing;
            if (PhotonNetwork.IsMasterClient)
                FlipCoin();

           

        }
        private void  FlipCoin()
        {
            var flip = UnityEngine.Random.value;
            RaiseEventOptions options = new RaiseEventOptions() { Receivers = ReceiverGroup.All };
            SendOptions send = new SendOptions(){ Reliability = true };
            PhotonNetwork.RaiseEvent(20, flip > 0.5f, options, send); 
            
        }
        private void SetSide(bool isMaster)
        {
            
            if (PhotonNetwork.IsMasterClient)
            {
                if (isMaster)
                    myColor = ChessColor.White;
                else
                    myColor = ChessColor.Black;
            }
            else
            {
                if (isMaster)
                    myColor = ChessColor.Black;
                else
                    myColor = ChessColor.White;
            }
            if(!isMaster)
                FindObjectOfType<Board>().transform.Rotate(0, 180, 0);

            controller.Init(myColor, this);
            PlaceFigures(isMaster);
        }

        


        private Figure SpawnFigure(GameObject prefab,Tile tile,ChessColor color)
        {
           var figure = Instantiate(prefab, tile.transform.position, Quaternion.identity).GetComponent<Figure>();
           figure.Init(tile, color);
           tile.Figure = figure;
           return figure;   
        }
        public void SpawnQueen(Tile tile, ChessColor color)
        {
            SpawnFigure(QueenPrefab, tile, color);
        }
        private List<Figure> SpawnSide(int side,ChessColor color,bool rotate=false)
        {
            int offset;
            if (side == 0) offset = 1;
            else offset = -1;
            var result = new List<Figure>();
            //pawns
            for (int i = 0; i < 8; i++)
            {
                result.Add(SpawnFigure(PawnPrefab, tiles[(side + offset) * 8 + i], color));
            }
            //rooks
            result.Add(SpawnFigure(RookPrefab, tiles[side * 8], color));
            result.Add(SpawnFigure(RookPrefab, tiles[side * 8+7], color));

            //knights
            result.Add(SpawnFigure(KnightPrefab, tiles[side * 8 + 1], color));
            result.Add(SpawnFigure(KnightPrefab, tiles[side * 8 + 6], color));

            //bishops
            result.Add(SpawnFigure(BishopPrefab, tiles[side * 8 + 2], color));
            result.Add(SpawnFigure(BishopPrefab, tiles[side * 8 + 5], color));
            //queen
            result.Add(SpawnFigure(QueenPrefab, tiles[side * 8 + 3], color));

            //king
            result.Add(SpawnFigure(KingPrefab, tiles[side * 8 + 4], color));
            if (rotate)
                foreach (var fig in result)
                {
                    fig.transform.Rotate(0, 180, 0);
                }
            return result;
        }
        private void PlaceFigures(bool isMaster)
        {

           whiteFigures = SpawnSide(0, ChessColor.White, false);
           blackFigures = SpawnSide(7, ChessColor.Black, true);
          
           if (!isMaster)
            {
                foreach (var item in whiteFigures)
                    item.transform.Rotate(0, 180, 0);

                foreach (var item in blackFigures)
                    item.transform.Rotate(0, 180, 0);
                
            }
            

          

        }

        public void PlayerWantMove(int start, int end)
        {
            SendOptions send = new SendOptions() { Reliability = true, Encrypt = true };
            RaiseEventOptions options= new RaiseEventOptions() { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(30, new int[] { start, end }, options, send);
           
        }

        private void MoveFigure(int start, int end)
        {
            var start_tile = tiles.Where(tile => tile.index == start).ToList();
            var end_tile = tiles.Where(tile => tile.index == end).ToList();
            start_tile[0].Figure.Move(end_tile[0]);
            start_tile[0].Figure = null;
  
        }

        public void OnEvent(EventData photonEvent)
        {
            switch (photonEvent.Code)
            {
                case 20:    
                    SetSide((bool)photonEvent.CustomData);
                    break;
                case 30:
                    var data = (int[])photonEvent.CustomData;
                    MoveFigure(data[0], data[1]);
                    break;
                case 100:
                    if ((ChessColor)(int)photonEvent.CustomData == myColor)
                    {
                        win_label.gameObject.SetActive(true);
                    }
                    else
                    {
                        lose_label.gameObject.SetActive(true);
                    }
                    Invoke("Leave", 5);
                    break;

            }
        }
        private void Leave()
        {
            PhotonNetwork.LeaveRoom();

        }

      
    }
}