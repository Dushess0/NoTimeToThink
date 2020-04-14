using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chess
{
    public class ChessGUI : MonoBehaviour
    {
        [HideInInspector]
        public List<Tile> possibleMoves;

        [SerializeField]
        private GameObject selector;
        private List<Tile> allTiles;
        [HideInInspector]
        public Tile lastSelected;
        [HideInInspector]
        public PlayerController player;

        private void ShowPossibleMoves(Tile t)
        {
            var fig = t.Figure;
            if (fig.color != player.color) return;
            lastSelected = t;
            possibleMoves = fig.GetPossibleMoves(allTiles);
            foreach (var tile in possibleMoves)
                tile.Select(selector);
  
        }
        private void Start()
        {
            allTiles = new List<Tile>();
            foreach (var tile in FindObjectsOfType<Tile>())
            {
                allTiles.Add(tile);
            }
            possibleMoves = new List<Tile>();
        }
      
        public bool ProcessTile(Tile tile) // returns true if move
        {  
            if (!tile.isSelected)
            {
                HideMoves();
                if (!tile.IsEmpty &&tile.Figure.ReadyToMove)
                {
                    ShowPossibleMoves(tile);
                }
                return false;
            }
             else
            {
                HideMoves();
                return true;
            }   
        }


        private void HideMoves()
        {
            foreach (var tile in possibleMoves)
                tile.Deselect();
            possibleMoves = new List<Tile>();
        }
     
    }
}
