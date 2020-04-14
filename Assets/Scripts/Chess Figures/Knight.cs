using Chess.Audio;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chess.Figures
{
    public class Knight : Figure
    {
        protected override void Start()
        {
            base.Start();
            this.Cooldown = 5;
            this.speed = 0.5f;

        }
       
        public override List<Tile> GetPossibleMoves(List<Tile> tiles)
        {

            var result = new List<Tile>();

            var g = tiles.Where(tile => (Mathf.Abs(tile.x - this.tile.x) == 2 && Mathf.Abs(tile.y - this.tile.y) == 1) || (Mathf.Abs(tile.x - this.tile.x) == 1 && Mathf.Abs(tile.y - this.tile.y) == 2));
            g = g.Where(tile => tile.IsEmpty || tile.Figure.color != this.color);
            result.AddRange(g);

            return result;
        }

        protected override void BeforeMove()
        {
            AudioManager.instance.Play("knight");
        }
    }
}