using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chess.UI
{
    public class TableClock : MonoBehaviour
    {
        [SerializeField]
        private TextMesh time;

        // Update is called once per frame
        void Update()
        {
            time.text = DateTime.UtcNow.ToLocalTime().ToString("H:mm");
        }
    }
}