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
    public class ChessManager : MonoBehaviourPunCallbacks
    {


        public GameState currentState;
        PlayerController whitePlayer;
        PlayerController blackPlayer;
        [SerializeField]
        List<Tile> tiles;

        public readonly Vector3 spawnOffset = Vector3.up*0.6f;


        public void StartGame()
        {
            currentState = GameState.Playing;
            ChooseSides();
            PlaceFigures();     

        }
        private void ChooseSides()
        {
            var players = FindObjectsOfType<PlayerController>();
            var flip = UnityEngine.Random.Range(0, 2);
            Debug.Log(flip);
            if (flip == 0)
            {
                whitePlayer = players[0];
                blackPlayer = players[1];
            }
            else
            {
                whitePlayer = players[1];
                blackPlayer = players[0];
            }
        }
        private void SpawnFigure(string name,Tile tile,ChessColor color)
        {

           var figure = PhotonNetwork.Instantiate(name, tile.transform.position, Quaternion.identity).GetComponent<Figure>();
           figure.Init(tile, color);
            




        }
        private void SpawnSide(int side,int offset,ChessColor color)
        {
           
            for (int i = 0; i < 8; i++)
            {
                SpawnFigure("Pawn", tiles[(side+offset)*8+ i], color);
            }

            //rooks
            //knights
            //bishops
            //queen
            //king


        }
        private void PlaceFigures()
        {
           
            if (whitePlayer.transform.position.y<0)  //master
            {
                SpawnSide(0,1, ChessColor.Black);
                SpawnSide(7,-1, ChessColor.White);
            }
            else    //client
            {

                SpawnSide(0,1, ChessColor.White);
                SpawnSide(7,-1, ChessColor.Black);
            }


        }

     
    }
}