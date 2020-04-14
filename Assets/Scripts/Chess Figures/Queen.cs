using Chess.Audio;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Chess.Figures
{
    public class Queen : Figure
    {
        protected override void Start()
        {
            base.Start();
            this.Cooldown = 6;
            this.speed = 1;

        }
        public override List<Tile> GetPossibleMoves(List<Tile> tiles)
        {

            var result = Rook.GetPossibleMoves(this.tile.x,this.tile.y,this.color,tiles);
            result.AddRange(Bishop.GetPossibleMoves(this.tile.x, this.tile.y, this.color, tiles));
            result.Remove(this.tile);

            return result;
        }
        protected override void BeforeMove()
        {
            AudioManager.instance.Play("queen");
        }

    }
}