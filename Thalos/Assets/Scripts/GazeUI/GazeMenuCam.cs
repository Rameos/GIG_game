using UnityEngine;
using System.Collections;
using iViewX;
using Controller;

namespace GazeGUI
{
    public class GazeMenuCam : MonoBehaviour
    {
        private Camera guicam;
        [SerializeField]
        private GameObject circleMenu;
        [SerializeField]
        private GameObject inventoryMenu;
        [SerializeField]
        private GameObject pauseMenu;

        private BaseGazeUI oldSelection;

        void Start()
        {
            guicam = gameObject.camera;
            closeAllMenus();
            Gamestatemanager.ChangeInGameMenuHandler += SetMenuState;
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

        void SetMenuState(int ID_Menu,bool status)
        {
            switch (ID_Menu)
            {
                case Constants.INGAMEMENU_CIRCLEMENU:
                    circleMenu.SetActive(status);
                    break;

                case Constants.INGAMEMENU_INVENTORY:
                    inventoryMenu.SetActive(status);
                    break;

                case Constants.INGAMEMENU_PAUSE:
                    pauseMenu.SetActive(status);
                    break;
            }
        }

        private void closeAllMenus()
        {
            circleMenu.SetActive(false);
            inventoryMenu.SetActive(false);
            pauseMenu.SetActive(false);
        }
    }

}