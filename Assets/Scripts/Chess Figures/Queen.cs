using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Chess.Figures
{
    public class Queen : Figure
    {
        public override List<Tile> GetPossibleMoves(List<Tile> tiles)
        {

            var result = Rook.GetPossibleMoves(this.tile.x,this.tile.y,this.color,tiles);
            result.AddRange(Bishop.GetPossibleMoves(this.tile.x, this.tile.y, this.color, tiles));



            result.Remove(this.tile);
           


            return result;
        }

    }
}