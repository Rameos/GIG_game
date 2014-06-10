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
            if(playerModel.checkIfDead()==true)
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
            playerModel.HealthPoints -= (damagepoints - playerModel.Armour);
        }
        #endregion
    }
}
