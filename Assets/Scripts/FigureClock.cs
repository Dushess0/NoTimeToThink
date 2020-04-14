using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chess.UI
{
    public class FigureClock : MonoBehaviour
    {
        private Figure figure;
        [SerializeField]
        private GameObject arrow;
        
        private float timer;
        public void Init(Figure figure)
        {
            this.figure = figure;
        }
        private void Update()
        {
            timer += Time.deltaTime;
            var percentage = timer/figure.Cooldown;
            arrow.transform.rotation = Quaternion.Euler(0, 0, 360*percentage );
        }
        public void Begin()
        {
            timer = 0;
            this.gameObject.SetActive(true);
        }
        public void Stop()
        {
            this.gameObject.SetActive(false);
        }

    }
}