using UnityEngine;
using System.Collections;
using Controller;
using Backend;

namespace GazeGUI
{
    public class GazeGUI_SelectionButton : BaseGazeUI
    {
        [SerializeField]
        private Constants.selectableItemsCircleMenu actionItem;
        
        private bool isSelectable = true;

        [SerializeField]
        private bool isEndless = false;

        [SerializeField]
        private GameObject Icon;
        
        [SerializeField]
        private GameObject CountText;



        public Color32 notSelectedColor;
        public Color32 selectedColor;
        public Color32 notSelectableColor;

        private Color32 destinationColor;
        private SpriteRenderer renderer;
        private PlayerModel.PhialType phialType;
        
        void Start()
        {
            renderer = GetComponent<SpriteRenderer>();
            destinationColor = notSelectedColor;

            phialType = convertCircleItemInPhial();
        }

        void FixedUpdate()
        {

            renderer.material.color = destinationColor;
        }
        void Update()
        {
            if (!isEndless)
            {
                isSelectable = getIsSelectable();
            }
            else
            {
                if (CountText!= null)
                CountText.SetActive(false); 
            }

            //renderer.material.color = Color32.Lerp(renderer.material.color,destinationColor,1f);
            Icon.renderer.material.color = Color.white;

            if(CountText!= null)
            {
                CountText.GetComponent<TextMesh>().text = PlayerModel.Instance().getCountOfPhialsOfSortInInventory(phialType)+"x";
            }
            
            if(!isSelectable)
            {

                CountText.SetActive(false);
                destinationColor = notSelectableColor;
                Icon.renderer.material.color = notSelectableColor;
            }
            else
            {
                if(CountText!=null)
                {
                    CountText.SetActive(true);
                }
            }


            
        }

        private bool getIsSelectable()
        {

            if(PlayerModel.Instance().checkIfPhialIsInInventory(phialType))
            {
                return true; 
            }
            
            return false;
        }

        private PlayerModel.PhialType convertCircleItemInPhial()
        {

            switch (actionItem)
            {
                case Constants.selectableItemsCircleMenu.HealPoison:
                    return PlayerModel.PhialType.Heal;

                case Constants.selectableItemsCircleMenu.FirePoison:
                    return PlayerModel.PhialType.Fire;

                case Constants.selectableItemsCircleMenu.IcePoison:
                    return PlayerModel.PhialType.Ice;

            }
            return PlayerModel.PhialType.Empty;
        }

        public override void OnGazeEnter()
        {
            if(isSelectable)
            {
                destinationColor = selectedColor;
            }

        }

        public override void OnGazeStay()
        {
            
        }

        public override void OnGazeExit()
        {
            if (isSelectable)
            {
                //Gamestatemanager.OnRumbleEventStop();
                destinationColor = notSelectedColor;
            }
        }

        public override void OnEventStart()
        {

            if(isSelectable)
            {
                Debug.Log("GazeEvent!");
                Gamestatemanager.OnSelectNewItem(actionItem);
//                UISoundManager.
            }
        }
    }
}