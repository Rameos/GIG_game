using UnityEngine;
using System.Collections;
using iViewX;
using Controller;

namespace GazeUI
{
    public class GazeMenuCam : MonoBehaviour
    {
        private Camera guicam;
        private BaseGazeUI oldSelection;

        void Start()
        {
            guicam = gameObject.camera;
        }

        void Update()
        {

            Vector2 gazePosOnScreen = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
            gazePosOnScreen.y = Screen.height-gazePosOnScreen.y;
            RaycastHit2D hit = Physics2D.Raycast(guicam.ScreenToWorldPoint(gazePosOnScreen), Vector2.zero);

            if (hit.collider != null&& hit.collider.tag =="GazeGui")
            {
                BaseGazeUI uiItem = hit.collider.gameObject.GetComponent<BaseGazeUI>();
                
                if (oldSelection == null)
                    oldSelection = uiItem;

                else if (uiItem != oldSelection)
                {
                    oldSelection.OnObjectExit();
                    oldSelection = uiItem;
                }

                uiItem.onObjectHit();

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    uiItem.OnEventStart();
                }

            }
        }
    }

}