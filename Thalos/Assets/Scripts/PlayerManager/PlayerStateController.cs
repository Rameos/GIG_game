using UnityEngine;
using UnityEngineInternal;
using System.Collections;
using Backend;

namespace Controller
{
    public class PlayerStateController : MonoBehaviour
    {
        private static PlayerStateController playerController;
        private PlayerModel playerModel;
        private GameObject audioManager;

        #region UNITY_FUNCTIONS
        void Start()
        {
            Debug.Log("PlayerStatemanager");
            Gamestatemanager.PlayerIsDeadHandler += Gamestatemanager_PlayerIsDeadHandler;
            Gamestatemanager.PlayerGetsDamageHandler += TakeDamage;
            Gamestatemanager.SelectNewItemHandler += Gamestatemanager_SelectItem;

            PlayerModel.Instance();
            
            
            
        }

        private void initAudioManager()
        {
            audioManager = GameObject.FindGameObjectWithTag("Audiomanager");
            
            if(audioManager != null)
            {
                audioManager.transform.parent = this.gameObject.transform;
                audioManager.transform.localPosition = Vector3.zero;
            }
        }

        void Update()
        {
            if(this.checkIfDead()==true)
            {
                Gamestatemanager.OnPlayerIsDead();
            }
        }

       void OnLevelWasLoaded(int level)
       {
           StartCoroutine(waitForLoadning());
       }
       

       
        #endregion

        #region PUBLIC_FIELDS
       
        public static PlayerStateController Instance()
        {
            if (playerController == null)
            {
                playerController = new PlayerStateController();
            }

            return playerController;
        }

        /// <summary>
        /// Check if Player is Dead
        /// </summary>
        /// <returns></returns>
        public bool checkIfDead()
        {
            if (playerModel.HealthPoints > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Remove HealthPoints
        /// </summary>
        /// <param name="Damage"></param>
        /// <returns></returns>
        public void TakeDamage(int Damage)
        {
            Debug.Log("HitPlayer");
            playerModel.HealthPoints -= Damage;
        }

        public int AddMaxHealthPoints(int HeartPiece)
        {
            playerModel.MaxHealthPoints += HeartPiece;
            return playerModel.MaxHealthPoints;
        }

        public int Heal(int HealPotion)
        {
            if(playerModel.getCountOfPhialsOfSortInInventory(PlayerModel.PhialType.Heal)>0)
            {
                if (playerModel.HealthPoints + HealPotion >= playerModel.MaxHealthPoints)
                {
                    playerModel.HealthPoints = playerModel.MaxHealthPoints;
                }

                else
                {
                    playerModel.HealthPoints += HealPotion;
                }

                playerModel.removePhialFromInventory(PlayerModel.PhialType.Heal);
                Debug.Log("Healitem:" + playerModel.getCountOfPhialsOfSortInInventory(PlayerModel.PhialType.Heal));
                return playerModel.HealthPoints;
            }

            return 0;
        }

        #endregion

        #region PRIVATE_FIELDS
        
        private PlayerStateController()
        {
            playerModel = PlayerModel.Instance();
        }

        private void Gamestatemanager_PlayerIsDeadHandler()
        {
            Debug.Log("PlayerIsDead!");
        }

        private void Gamestatemanager_SelectItem(Constants.selectableItemsCircleMenu selectedItem)
        {
            Debug.Log("SelectITEM");

            switch (selectedItem)
            {
                case Constants.selectableItemsCircleMenu.HealPoison:
                    Heal(Constants.healPower);
                    break;
 
                case  Constants.selectableItemsCircleMenu.NormalBolt:
                    playerModel.DamageType_Bolt = PlayerModel.DamageTypes.Standard;
                    break;

                case Constants.selectableItemsCircleMenu.FireBolt:
                    playerModel.DamageType_Bolt = PlayerModel.DamageTypes.Fire;
                    break;

                case Constants.selectableItemsCircleMenu.IceBolt:
                    playerModel.DamageType_Bolt = PlayerModel.DamageTypes.Ice;
                    break;

                case Constants.selectableItemsCircleMenu.FirePoison:
                    playerModel.DamageType_Poision = PlayerModel.DamageTypes.Fire;
                    break;

                case Constants.selectableItemsCircleMenu.IcePoison:
                    playerModel.DamageType_Poision = PlayerModel.DamageTypes.Ice;
                    break;                    
            }
        }

        private void setPoisionDamage(PlayerModel.DamageTypes type)
        {

        }
        #endregion


        IEnumerator waitForLoadning()
        {
            yield return new WaitForSeconds(2);
            initAudioManager();
            Gamestatemanager.OnOpenPlayView();
        }
    }
}
