using UnityEngine;
using System.Collections;
using Controller; 

namespace GazeGUI
{
    public class GazeGUI_CircleMenu : MonoBehaviour
    {
        private Animator animator;
        void Start()
        {
            animator = GetComponent<Animator>();
            Gamestatemanager.ChangeInGameMenuHandler += CloseMenu;
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