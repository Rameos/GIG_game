﻿using UnityEngine;
using System.Collections;
using Controller;

namespace GazeGUI
{
    public class GazeGUI_SelectionButton : BaseGazeUI
    {
        [SerializeField]
        private Constants.selectableItemsCircleMenu actionItem;
        private bool isSelectable = true;



        public override void OnGazeEnter()
        {
            Debug.Log("GazeEnter!");
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

            Debug.Log("GazeEvent!");
            if(isSelectable)
            {
                Gamestatemanager.OnSelectNewItem(actionItem);
//                UISoundManager.
            }
        }
    }
}