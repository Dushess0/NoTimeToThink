using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class ChessGUI : MonoBehaviour
    {
        public List<Tile> possibleMoves;

        [SerializeField]
        private GameObject selector;
        private List<Tile> allTiles;
        public Tile lastSelected;

        private void ShowPossibleMoves(Tile t)
        {
            var fig = t.Figure;
            if (!fig) return;
            lastSelected = t;
            possibleMoves = fig.GetPossibleMoves(allTiles);
            foreach (var tile in possibleMoves)
            {
                tile.Select(selector);
            }
           
        }
        private void Start()
        {
            allTiles = new List<Tile>();
            foreach (var tile in FindObjectsOfType<Tile>())
            {
                allTiles.Add(tile);
            }
        }
        public bool ProcessTile(Tile tile) // returns true if move
        {
            if (!tile.isSelected)
            {
                HideMoves();
                ShowPossibleMoves(tile);
                return false;
            }
             else
            {
                return true;
            }
            
        }
        private void HideMoves()
        {
            foreach (var tile in possibleMoves)
            {
                tile.Deselect();
            }
            possibleMoves = new List<Tile>();
        }
        
    }
}
