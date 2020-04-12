using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
namespace Chess
{

    public class PlayerController : MonoBehaviourPun
    {
        [SerializeField]
        private Camera cam;
        private ChessColor color;
     

       public void Init(ChessColor chessColor)
        {
           
            color = chessColor;
        }
       public void EnableCamera()
        {
            cam.gameObject.SetActive(true);
        }
    }
}