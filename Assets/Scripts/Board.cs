using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Chess
{
    public class Board : MonoBehaviour     /// 0.385   0.05  0.385 = start
    {
        float distance_between = 0.01f;
        float size = 0.1f;
        [SerializeField]
        GameObject start;
        //void Start()
        //{
        //    for (int i = 0; i < 8; i++)
        //    {
        //        for (int j = 0; j < 8; j++)
        //        {
        //            var tile =Instantiate(start, start.transform.position + new Vector3(i*(distance_between+size),0, -j * (distance_between + size)),Quaternion.identity,this.transform);
        //            var tilecomponent = tile.AddComponent<Tile>();
        //            tilecomponent.x = i;
        //            tilecomponent.y = j;
        //        }
        //    }

        //    PrefabUtility.SaveAsPrefabAsset(this.gameObject, "board.prefab");\
            // very usefull! 

        //}

       
    }
}