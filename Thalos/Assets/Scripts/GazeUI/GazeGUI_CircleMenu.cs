using UnityEngine;
using System.Collections;
using Controller; 

namespace GazeGUI
{
    public class GazeGUI_CircleMenu : MonoBehaviour
    {
        [SerializeField]
        private GazeMenuCam camScript;
        [SerializeField]
        private bool isGazeActive = false;
        [SerializeField]
        private Vector2 healMax;
        [SerializeField]
        private Vector2 healMin;
       
        [SerializeField]
        private Vector2 firePoisonMax;
        [SerializeField]
        private Vector2 firePoisonMin;
        
        [SerializeField]
        private Vector2 icePoisonMax;
        [SerializeField]
        private Vector2 icePoisonMin;

        [SerializeField]
        private Vector2 standardBoltMax;
        [SerializeField]
        private Vector2 standardBoltMin;

        [SerializeField]
        private Vector2 fireBoltMax;
        [SerializeField]
        private Vector2 fireBoltMin;

        [SerializeField]
        private Vector2 iceBoltMax;
        [SerializeField]
        private Vector2 iceBoltMin;


        [SerializeField]
        private GameObject[] buttons;

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

        void Update()
        {
            BaseGazeUI selectionGUI = null; 
            if(!isGazeActive)
            {
                float rightX = Input.GetAxis("RightStickX");
                float rightY = -Input.GetAxis("RightStickY");

                Vector2 positionRightStic = new Vector2(rightX, rightY);


                //Poison
                //
                //Heal
                if (healMax.x >= positionRightStic.x && healMax.y >= positionRightStic.y && healMin.x < positionRightStic.x && healMin.y < positionRightStic.y)
                {
                    Debug.Log("HEAL POISON");

                    refreshSelection();


                    selectionGUI = buttons[3].GetComponent<GazeGUI.GazeGUI_SelectionButton>();
                    buttons[3].GetComponent<GazeGUI.GazeGUI_SelectionButton>().OnGazeEnter();
                }

                //FirePoison
                else if (firePoisonMax.x >= positionRightStic.x && firePoisonMax.y >= positionRightStic.y && firePoisonMin.x < positionRightStic.x && firePoisonMin.y < positionRightStic.y)
                {
                    Debug.Log("FIRE POISON");
                    refreshSelection();
                    selectionGUI = buttons[4].GetComponent<GazeGUI.GazeGUI_SelectionButton>();
                
                    buttons[4].GetComponent<GazeGUI.GazeGUI_SelectionButton>().OnGazeEnter();
                }

                //icePoison
                else if (icePoisonMax.x >= positionRightStic.x && icePoisonMax.y >= positionRightStic.y && icePoisonMin.x < positionRightStic.x && icePoisonMin.y < positionRightStic.y)
                {
                    Debug.Log("ICE POISON");
                    refreshSelection();
                    selectionGUI = buttons[5].GetComponent<GazeGUI.GazeGUI_SelectionButton>();
                
                    buttons[5].GetComponent<GazeGUI.GazeGUI_SelectionButton>().OnGazeEnter();
                }

                //Bolt
                //
                //Heal
                if (standardBoltMax.x >= positionRightStic.x && standardBoltMax.y >= positionRightStic.y && standardBoltMin.x < positionRightStic.x && standardBoltMin.y < positionRightStic.y)
                {
                    Debug.Log("StandardBolt");
                    refreshSelection();
                    selectionGUI = buttons[2].GetComponent<GazeGUI.GazeGUI_SelectionButton>();
                
                    buttons[2].GetComponent<GazeGUI.GazeGUI_SelectionButton>().OnGazeEnter();
                }

                //FirePoison
                else if (fireBoltMax.x >= positionRightStic.x && fireBoltMax.y >= positionRightStic.y && fireBoltMin.x < positionRightStic.x && fireBoltMin.y < positionRightStic.y)
                {
                    Debug.Log("FIRE Bolt");
                    refreshSelection();

                    selectionGUI = buttons[0].GetComponent<GazeGUI.GazeGUI_SelectionButton>();
                
                    buttons[0].GetComponent<GazeGUI.GazeGUI_SelectionButton>().OnGazeEnter();
                }

                //icePoison
                else if (iceBoltMax.x >= positionRightStic.x && iceBoltMax.y >= positionRightStic.y && iceBoltMin.x < positionRightStic.x && iceBoltMin.y < positionRightStic.y)
                {
                    Debug.Log("ICE Bolt");
                    refreshSelection();

                    selectionGUI = buttons[1].GetComponent<GazeGUI.GazeGUI_SelectionButton>();
                
                    buttons[1].GetComponent<GazeGUI.GazeGUI_SelectionButton>().OnGazeEnter();
                }

                camScript.actualSelection = selectionGUI;
                
            }
 

        }

        void refreshSelection()
        {
            foreach(GameObject e in buttons)
            {
                e.GetComponent<GazeGUI_SelectionButton>().OnGazeExit();
            }
        }
    }

}