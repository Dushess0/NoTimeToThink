using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Chess.Audio;
namespace Chess.Figures
{
    public class King : Figure
    {
        protected override void Start()
        {
            base.Start();
            this.Cooldown = 5;
            this.speed = 1;

        }
        public override void Death()
        {
            base.Death();
            RaiseEventOptions options = new RaiseEventOptions() { Receivers = ReceiverGroup.All };
            SendOptions send = new SendOptions() { Encrypt = true, Reliability = true };
            PhotonNetwork.RaiseEvent(100, (int)this.color, options, send);
            AudioManager.instance.Play("king death");

            


        }
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