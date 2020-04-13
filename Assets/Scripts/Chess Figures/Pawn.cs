using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Chess.Figures
{
    public class Pawn : Figure
    {
        
        public override List<Tile> GetPossibleMoves(List<Tile> tiles)
        {

            var result = new List<Tile>();
            int offset;
            int en_passant_pos;
            if (color == ChessColor.White)
            {
                offset = 1;
                en_passant_pos = 1; 
            }
            else 
            {
                offset = -1;
                en_passant_pos = 6; 
            }

            var forward = tiles.Where(tile => (this.tile.y + offset == tile.y || (this.tile.y == en_passant_pos && tile.y == this.tile.y + offset * 2)) && tile.x == this.tile.x);
            CheckDirection(forward, result);

            var canAttack =tiles.Where(tile=>Mathf.Abs(this.tile.x-tile.x)==1 && this.tile.y + offset == tile.y && !tile.IsEmpty && tile.Figure.color!=this.color);
            result.AddRange(canAttack);

            return result;
        }


    }
}