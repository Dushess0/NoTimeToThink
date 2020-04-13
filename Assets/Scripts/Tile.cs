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
        public int index { get => x + y * 8; }

        public bool isSelected { get; private set; }

        private GameObject selector;

        //private void Start()
        //{
        //    gameObject.AddComponent<BoxCollider>().size= new Vector3()
        //}

        public bool IsEmpty { get =>Figure == null;}
            
       
        public void Select(GameObject sel)
        {
            isSelected = true;
            selector = Instantiate(sel, this.transform); ;
           

        }

        public void Deselect()

        {
            isSelected = false;
            Destroy(selector);
            selector = null;

        }
        

    }
   
}