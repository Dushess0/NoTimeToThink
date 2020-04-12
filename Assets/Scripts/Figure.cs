using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Chess
{
    public class Figure : MonoBehaviour, IPunObservable
    {

        [SerializeField]
        protected Material whiteMaterial;
        [SerializeField]
        protected Material blackMaterial;

        [SerializeField]
        protected GameObject graphics;

        [SerializeField]
        protected MeshRenderer renderer;


        protected ChessColor color;
        protected Tile tile;


        protected bool isClickable;
        


        public virtual List<Tile> GetPossibleMoves(List<Tile> tiles)
        {
            return new List<Tile>();
        }

        
        
        public void Init(Tile tile,ChessColor color)
        {

            this.tile = tile;
            this.color = color;
            ChangeColor();
               
        }
     
       
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
           
            if (stream.IsWriting)
            {
                stream.SendNext(color);
                stream.SendNext(isClickable);
            }
           else
            {

                color=(ChessColor)stream.ReceiveNext();
                ChangeColor();
                
                isClickable = (bool)stream.ReceiveNext();
                
                

            }
        }

      
        private void ChangeColor()
        {
            if (color == ChessColor.Black)
                renderer.material = blackMaterial;
            else
                renderer.material = whiteMaterial;
            
        }
    }
}