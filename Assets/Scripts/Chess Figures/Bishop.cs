using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Chess.Figures
{
    public class Bishop : Figure
    {
        public override List<Tile> GetPossibleMoves(List<Tile> tiles)
        {

            var result = GetPossibleMoves(this.tile.x, this.tile.y, this.color, tiles);
            result.Remove(this.tile);

            return result;
        }
        public static List<Tile>  GetPossibleMoves(int x,int y,ChessColor color,List<Tile> tiles)
        {
            var result= new List<Tile>();


            //all mathching by color tiles

            
            var diag = tiles.Where(tile => Mathf.Abs(tile.x - x) == Mathf.Abs(tile.y - y));
            diag = diag.Where(tile => tile.x != x && tile.y != y).OrderBy(tile => Mathf.Abs(tile.x - x) + Mathf.Abs(tile.y - y)); 
            IEnumerable<Tile> left, right, bottom, top;

            left = diag.Where(tile=>tile.x > x && tile.y > y);
            right =diag.Where(tile=>tile.x > x && tile.y < y);
            bottom=diag.Where(tile=>tile.x < x && tile.y > y);
            top =  diag.Where(tile => tile.x < x && tile.y < y);

            CheckDirection(left, result  ,color);
            CheckDirection(right, result ,color);
            CheckDirection(bottom, result,color);
            CheckDirection(top, result   ,color);

            return result;

        }

    }
}
