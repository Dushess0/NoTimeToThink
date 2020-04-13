using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Chess.Figures
{
    public class King : Figure
    {
        public override List<Tile> GetPossibleMoves(List<Tile> tiles)
        {

            var result = new List<Tile>();

            var square = tiles.Where(tile=>Mathf.Abs(this.tile.x - tile.x) < 2 && Mathf.Abs(this.tile.y - tile.y) < 2);

            square = square.Where(tile=>tile.IsEmpty || (tile.Figure.color != this.color));

            result.AddRange(square);
           


            return result;
        }
    }
}