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


        #region UNITY_FUNCTIONS
        void Start()
        {
            Gamestatemanager.PlayerIsDeadHandler += Gamestatemanager_PlayerIsDeadHandler;
            Gamestatemanager.PlayerGetsDamageHandler += ApplyDamageToModel;
        }

        void Update()
        {
            if(this.checkIfDead()==true)
            {
                Gamestatemanager.OnPlayerIsDead();
            }
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
        /// Remove HealthPoints (
        /// </summary>
        /// <param name="Damage"></param>
        /// <returns></returns>
        public int TakeDamage(int Damage)
        {
            playerModel.HealthPoints -= Damage;
            return playerModel.HealthPoints;
        }

        public int AddMaxHealthPoints(int HeartPiece)
        {
            playerModel.MaxHealthPoints += HeartPiece;
            return playerModel.MaxHealthPoints;
        }

        public int Heal(int HealPotion)
        {
            if (playerModel.HealthPoints + HealPotion >= playerModel.MaxHealthPoints)
            {
                playerModel.HealthPoints = playerModel.MaxHealthPoints;
            }
            else
            {
                playerModel.HealthPoints += HealPotion;
            }
            return playerModel.HealthPoints;
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

        private void ApplyDamageToModel(int damagepoints)
        {
            playerModel.HealthPoints -= damagepoints;
        }
        #endregion
    }
}
