using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chess
{
    public class Tile : MonoBehaviour
    {

        public Figure Figure;
        public int x;
        public int y;



        public void Init(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
        public bool isEmpty()
        {
            return Figure == null;
        }
        

    }
   
}