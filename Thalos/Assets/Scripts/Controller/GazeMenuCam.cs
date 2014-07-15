using UnityEngine;
using System.Collections;
using iViewX;
using Controller;

namespace GazeGUI
{
    public class GazeMenuCam : MonoBehaviour
    {
        Camera guicam;

        void Start()
        {
            guicam = gameObject.camera;
        }

        void Update()
        {

            Vector2 gazePosOnScreen = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
            RaycastHit2D hit = Physics2D.Raycast(guicam.ScreenToWorldPoint(gazePosOnScreen), Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Target Position: " + hit.collider.gameObject.name);
            }

            /*Debug.Log("updateGUICAM");

            
            
            
            RaycastHit2D hit2D = new RaycastHit2D();
            if (Physics2D.Raycast(new Vector2(123, 123), new Vector2(1, 1)))
            {
                if (hit2D != null)
                {
                    Debug.Log("Hit2D: " + hit2D.collider.gameObject.name);
                }
            }*/



        }
    }

}