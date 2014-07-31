using UnityEngine;
using System.Collections;
using Controller;

namespace GazeGUI
{
    public class GazeGUI_SelectionButton : BaseGazeUI
    {
        [SerializeField]
        private Constants.selectableItemsCircleMenu actionItem;
        private bool isSelectable = true;

        private GameObject Icon;
        private GameObject CountText;

        public Color32 notSelectedColor;
        public Color32 selectedColor;

        private Color32 destinationColor;
        private SpriteRenderer renderer;

        void Start()
        {
            renderer = GetComponent<SpriteRenderer>();
            destinationColor = notSelectedColor;
        }

        void Update()
        {
            renderer.material.color = Color32.Lerp(renderer.material.color,destinationColor,1f);
        }

        public override void OnGazeEnter()
        {
            destinationColor = selectedColor; 

            Debug.Log("GazeEnter!");
               //FancyAnimation
        }

        public override void OnGazeStay()
        {
            
        }

        public override void OnGazeExit()
        {

            destinationColor = notSelectedColor;
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