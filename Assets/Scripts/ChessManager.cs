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
        PlayerController whitePlayer;
        PlayerController blackPlayer;

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



       
        public void StartGame()
        {
          
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
            var players = FindObjectsOfType<PlayerController>();
            if (isMaster)
            {
              
                whitePlayer = players[0];
                blackPlayer = players[1];
            }
            else
            {
               
                whitePlayer = players[1];
                blackPlayer = players[0];
            }

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

            PlaceFigures();

        }

        


        private Figure SpawnFigure(GameObject prefab,Tile tile,ChessColor color)
        {
           
           var figure = Instantiate(prefab, tile.transform.position, Quaternion.identity).GetComponent<Figure>();
           figure.Init(tile, color);
           tile.Figure = figure;
           return figure;   
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
        private void PlaceFigures()
        {

           whiteFigures = SpawnSide(0, ChessColor.White, false);
           blackFigures = SpawnSide(7, ChessColor.Black, true);
        }

        public void PlayerDoMove(ChessColor player,int start,int end)
        {

        }

        public void OnEvent(EventData photonEvent)
        {
            switch (photonEvent.Code)
            {
                case 20: //
                  
                    SetSide((bool)photonEvent.CustomData);
                    break;
            }
        }
    }
}