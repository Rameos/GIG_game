using UnityEngine;
using System.Collections;
using Controller; 

namespace GazeGUI
{
    public class GazeGUI_CircleMenu : MonoBehaviour
    {
        void Start()
        {
            Gamestatemanager.ChangeInGameMenuHandler += CloseMenu;
        }

        void OnEnable()
        {
            Debug.Log("FadeInAnimation");
        }

        void CloseMenu(int ID_Menu, bool status)
        {
            if(ID_Menu == Constants.INGAMEMENU_CIRCLEMENU && status == false)
            {
                Debug.Log("CloseCircleMenu");
            }
        }
    }

}