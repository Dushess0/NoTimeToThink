using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Chess.UI;
using Chess.Audio;

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
        
        public float Cooldown { get; protected set; }
        public bool ReadyToMove { get; protected set; }
        protected float speed;
        protected float timer=0;
        [SerializeField]
        protected FigureClock clock;

        public virtual List<Tile> GetPossibleMoves(List<Tile> tiles)
        {
            return new List<Tile>();
        }
        /// <summary>
        /// Dont forget to use base.Start() for proper initialization of components
        /// </summary>
        protected virtual void Start()
        {

            ReadyToMove = true;
            graphics = transform.GetChild(0).gameObject;
            clock.Init(this);
            clock.Stop();

           // var photonview = GetComponent<PhotonView>();
           //var transformView = gameObject.AddComponent<PhotonTransformView>();
           // photonview.ObservedComponents.Add(transformView);
          
        }


        private void Update()
        {
            if (!ReadyToMove)
            {
                timer += Time.deltaTime;
                if (timer > Cooldown)
                {
                    ReadyToMove = true;
                    clock.Stop();
                }
            }
        }
        public static void CheckDirection(IEnumerable<Tile> to_check, List<Tile> accumulator, ChessColor color)
        {
            foreach (var item in to_check)
            {
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
        public void Move(Tile end)
        {
            BeforeMove();
            StartCoroutine(MovingCoroutine(transform.position, end));

        }
        protected virtual void BeforeMove()
        {

        }
        IEnumerator MovingCoroutine(Vector3 start,Tile end)
        {
            for (float i = 0; i < 1; i+=Time.deltaTime/speed)
            {
                transform.position = Vector3.Lerp(start, end.transform.position, i);
                yield return null;
            }
            transform.position = end.transform.position;
            if (!end.IsEmpty)
                end.Figure.Death();
            end.Figure = this;
            this.tile = end;
            timer = 0;
            ReadyToMove = false;
            clock.Begin();
            AfterMove();
        }
        protected virtual void AfterMove()
        {
            
        }
           
       
        public virtual void Death()
        {
            this.tile.Figure = null;
            Destroy(this.gameObject);

            AudioManager.instance.Play("death");
        }
        public void Init(Tile tile,ChessColor color)
        {

            this.tile = tile;
            this.color = color;
            ChangeColor();
           
        }
        private void ChangeColor()
        {
            if (color == ChessColor.Black)
                render.material = blackMaterial;
            else
                render.material = whiteMaterial;
        }

      
    }
}