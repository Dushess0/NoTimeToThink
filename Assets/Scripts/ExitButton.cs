using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.UI
{
    public class ExitButton : MonoBehaviour
    {
        private Camera cam;
        private BoxCollider mycollider;
        private void Start()
        {
            mycollider = GetComponent<BoxCollider>();
        }
        private void Update()
        {
            if (cam == null)
                cam = Camera.main;

            if (Input.GetMouseButtonDown(0))
            {
                var ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if ((BoxCollider)hit.collider == mycollider)
                    {
                        Leave();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
                Leave();

        }
        private void Leave()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}