using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Chess
{
    public class Figure : MonoBehaviour
    {

        [SerializeField]
        protected Material whiteMaterial;
        [SerializeField]
        protected Material blackMaterial;

        
        protected GameObject graphics;
        [SerializeField]
        protected MeshRenderer render;


        public ChessColor color { get; protected set; }
        protected Tile tile;
        protected bool isClickable;
        


        public virtual List<Tile> GetPossibleMoves(List<Tile> tiles)
        {
            return new List<Tile>();
        }
        /// <summary>
        /// Dont forget to use base.Start() for proper initialization of components
        /// </summary>
        protected virtual void Start()
        {
            graphics = transform.GetChild(0).gameObject;
            var photonview = GetComponent<PhotonView>();
            var transformView = gameObject.AddComponent<PhotonTransformView>();
            photonview.ObservedComponents.Add(transformView);
          
        }
        


        public static void CheckDirection(IEnumerable<Tile> to_check, List<Tile> accumulator, ChessColor color)
        {
            foreach (var item in to_check)
            {
                Debug.LogFormat("x={0},y={1}", item.x, item.y);
                if (item.IsEmpty)
                    accumulator.Add(item);
                else if (item.Figure.color != color)
                {
                    accumulator.Add(item);
                    break;
                }
                else
                    break;
            }

        }
        protected void CheckDirection(IEnumerable<Tile> to_check, List<Tile> accumulator)
        {
            CheckDirection(to_check, accumulator, this.color);
        }

        public void Init(Tile tile,ChessColor color)
        {

            this.tile = tile;
            this.color = color;
            ChangeColor();
           
        }
     
       
        //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        //{
           
        //    if (stream.IsWriting)
        //    {
        //        stream.SendNext(color);
        //        stream.SendNext(isClickable);
        //    }
        //   else
        //    {
        //        color=(ChessColor)stream.ReceiveNext();     
        //        isClickable = (bool)stream.ReceiveNext();
        //        ChangeColor();
        //    }
        //}

      
        private void ChangeColor()
        {
            if (color == ChessColor.Black)
                render.material = blackMaterial;
            else
                render.material = whiteMaterial;
            
        }

      
    }
}