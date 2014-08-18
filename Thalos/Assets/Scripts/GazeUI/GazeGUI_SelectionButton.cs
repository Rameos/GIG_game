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

        void Update()
        {
            //setSelectable only Endless Items
            if (!isEndless)
            {
                isSelectable = getIsSelectable();
            }
                //Only the Normal Bolt is real endless
            else if (isEndless && actionItem != Constants.selectableItemsCircleMenu.NormalBolt)
            {
                isSelectable = getIsSelectable();
            }
            else
            {
                if (CountText!= null)
                {
                    CountText.SetActive(false); 
                }
            }

            // change Material Color 
            renderer.material.color = Color32.Lerp(renderer.material.color, destinationColor, 1f);
            Icon.renderer.material.color = Color.white;

            
            if(CountText!= null)
            {
                CountText.GetComponent<TextMesh>().text = PlayerModel.Instance().getCountOfPhialsOfSortInInventory(phialType)+"x";
            }
            
            if(!isSelectable)
            {

                if (CountText != null)
                {
                    CountText.SetActive(false); 
                }

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
            Debug.Log("ITEM:" + phialType.ToString());
            if (PlayerModel.Instance().checkIfPhialIsInInventory(phialType)&& !isEndless)
            {
                return true;
            }

            else if (PlayerModel.Instance().getIsRecipeFounded(phialType.ToString()) && isEndless)
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
                case Constants.selectableItemsCircleMenu.FireBolt:
                    return PlayerModel.PhialType.Fire;

                case Constants.selectableItemsCircleMenu.IcePoison:
                case Constants.selectableItemsCircleMenu.IceBolt:
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