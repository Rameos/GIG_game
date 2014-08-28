using UnityEngine;
using System.Collections;
using Backend; 

namespace Controller
{
    public class ShotManager_Player : MonoBehaviour
    {
        [SerializeField]
        private GameObject BoltNormal;
        [SerializeField]
        private GameObject BoltFire;
        [SerializeField]
        private GameObject BoltIce;

        [SerializeField]
        private Transform instanciatePointBullet;
        [SerializeField]
        private Transform instanciatePointPoison;

        [SerializeField]
        private GameObject PoisonFire;
        [SerializeField]
        private GameObject PoisonIce;

        [SerializeField]
        private ShotAndThrowWithGaze gazeInputManagerShooting;

        [SerializeField]
        private bool isEndlessMunition = false;
        
        public void ShootBullet()
        {
           // GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).GetComponent<LogInputsFromController>().logShoot();

            PlayerModel.DamageTypes damage = PlayerModel.Instance().DamageType_Bolt;
            GameObject instance;
            Vector3 direction = gazeInputManagerShooting.directionShoot;

            //Standard Way
            switch (damage)
            {
                case PlayerModel.DamageTypes.Fire:
                    instance = GameObject.Instantiate(BoltFire, instanciatePointBullet.position, instanciatePointBullet.localRotation) as GameObject;
                    instance.GetComponent<Bullet>().Init(direction, Constants.ID_PLAYER, new Damage(Constants.damageFireBolt, PlayerModel.DamageTypes.Fire));
                    break;

                case PlayerModel.DamageTypes.Ice:
                    instance = GameObject.Instantiate(BoltIce, instanciatePointBullet.position, instanciatePointBullet.localRotation) as GameObject;
                    instance.GetComponent<Bullet>().Init(direction, Constants.ID_PLAYER, new Damage(Constants.damageIceBolt, PlayerModel.DamageTypes.Ice));
                    break;

                case PlayerModel.DamageTypes.Standard:
                    instance = GameObject.Instantiate(BoltNormal, instanciatePointBullet.position, instanciatePointBullet.localRotation) as GameObject;
                    instance.GetComponent<Bullet>().Init(direction, Constants.ID_PLAYER, new Damage(Constants.damageStandardBolt, PlayerModel.DamageTypes.Standard));

                    break;
            }


            Vector3 gazeDirection = gazeInputManagerShooting.destinationPoint_Shoot-transform.position;
            gazeDirection.y = 0f;
            float step = 0.4f * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, gazeDirection, 1,0.0f);
            this.gameObject.transform.rotation = Quaternion.LookRotation(newDir);

            gameObject.GetComponent<PlayerInputManager>().startRumbleForTime(0.1f, 0, 0.1f);
           
        }

        public void ThrowPoison()
        {
            //GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).GetComponent<LogInputsFromController>().logThrow();

                PlayerModel.DamageTypes damage = PlayerModel.Instance().DamageType_Poision;
                GameObject instance;
                PlayerModel.PhialType type = PlayerModel.Instance().convertDamageTypeToPhialType();

                if (PlayerModel.Instance().getCountOfPhialsOfSortInInventory(type) > 0)
                {
                    Vector3 direction = gazeInputManagerShooting.directionPoison;
                    switch (damage)
                    {
                        case PlayerModel.DamageTypes.Fire:
                            instance = GameObject.Instantiate(PoisonFire, instanciatePointPoison.position, instanciatePointPoison.localRotation) as GameObject;
                            instance.GetComponent<Poison>().Init(direction, new Damage(Constants.damageIcePoison, PlayerModel.DamageTypes.Fire), Constants.ID_PLAYER, gazeInputManagerShooting.destinationPoint_Poison);

                            break;

                        case PlayerModel.DamageTypes.Ice:
                            instance = GameObject.Instantiate(PoisonIce, instanciatePointPoison.position, instanciatePointPoison.localRotation) as GameObject;
                            instance.GetComponent<Poison>().Init(direction, new Damage(Constants.damageIceBolt, PlayerModel.DamageTypes.Ice), Constants.ID_PLAYER, gazeInputManagerShooting.destinationPoint_Poison);
                            break;

                        case PlayerModel.DamageTypes.Standard:
                        case PlayerModel.DamageTypes.None:
                            Debug.Log("Nope!");
                            break;
                    }

                    if(!isEndlessMunition)
                    {
                        PlayerModel.Instance().removePhialFromInventory(type);
                    }
                }

        }

    }
}
