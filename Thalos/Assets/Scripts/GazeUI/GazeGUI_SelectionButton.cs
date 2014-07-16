using UnityEngine;
using System.Collections;
using Controller;

namespace GazeGUI
{
    public class GazeGUI_SelectionButton : BaseGazeUI
    {
        [SerializeField]
        private Constants.item actionItem;
        private bool isSelectable;



        public override void OnGazeEnter()
        {
               //FancyAnimation
        }

        public override void OnGazeStay()
        {

        }

        public override void OnGazeExit()
        {
            //FancyAnimation
        }

        public override void OnEventStart()
        {
            if(isSelectable)
            {
                Gamestatemanager.OnSelectNewItem(actionItem);
//                UISoundManager.
            }
        }
    }
}