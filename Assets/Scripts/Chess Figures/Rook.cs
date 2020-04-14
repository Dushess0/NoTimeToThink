using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Chess.Audio;

namespace Chess.Figures
{
    public class Rook : Figure
    {
        protected override void Start()
        {
            base.Start();
            this.Cooldown = 6;
            this.speed = 2;

        }
        public override List<Tile> GetPossibleMoves(List<Tile> tiles)
        {

            var result = GetPossibleMoves(this.tile.x, this.tile.y, this.color, tiles);
            result.Remove(this.tile);


            return result;
        }

        public static List<Tile> GetPossibleMoves(int x, int y, ChessColor color, List<Tile> tiles)
        {

            var result = new List<Tile>();

            var lines = tiles.Where(tile => tile.x == x || tile.y == y).OrderBy(tile=>Mathf.Abs(tile.x - x) + Mathf.Abs(tile.y - y));



            IEnumerable<Tile> left, right, top, bottom;

            left =   lines.Where(tile => tile.x < x);
            right =  lines.Where(tile=>tile.x > x);
            top =    lines.Where(tile=>tile.y > y);
            bottom = lines.Where(tile=>tile.y < y);

            CheckDirection(left, result,color);
            CheckDirection(right, result ,color);
            CheckDirection(top,   result   ,color);
            CheckDirection(bottom, result,color);


            return result;
        }
        protected override void BeforeMove()
        {
            AudioManager.instance.Play("rook");
        }
    


    }
}