using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Chess.UI
{
    public class TableClock : MonoBehaviour
    {
        [SerializeField]
        private Text time;

        // Update is called once per frame
        void Update()
        {
            time.text = DateTime.UtcNow.ToLocalTime().ToString("H:mm");
        }
    }
}