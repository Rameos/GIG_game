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

        public void ShootBullet()
        {

            PlayerModel.DamageTypes damage = PlayerModel.Instance().DamageType_Bolt;
            GameObject instance;
            Vector3 direction = gazeInputManagerShooting.directionShoot;
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

            gameObject.GetComponent<PlayerInputManager>().startRumbleForTime(0.1f, 0, 0.1f);
        
        }

        public void ThrowPoison()
        {
                PlayerModel.DamageTypes damage = PlayerModel.Instance().DamageType_Poision;
                GameObject instance;

                Vector3 direction = gazeInputManagerShooting.directionPoison;
                switch (damage)
                {
                    case PlayerModel.DamageTypes.Fire:
                        instance = GameObject.Instantiate(PoisonFire, instanciatePointPoison.position, instanciatePointPoison.localRotation) as GameObject;
                        instance.GetComponent<Poison>().Init(direction, new Damage(Constants.damageIcePoison, PlayerModel.DamageTypes.Fire), Constants.ID_PLAYER);

                        break;

                    case PlayerModel.DamageTypes.Ice:
                        instance = GameObject.Instantiate(PoisonIce, instanciatePointPoison.position, instanciatePointPoison.localRotation) as GameObject;
                        instance.GetComponent<Poison>().Init(direction, new Damage(Constants.damageIceBolt, PlayerModel.DamageTypes.Ice), Constants.ID_PLAYER);
                        break;

                    case PlayerModel.DamageTypes.Standard:
                    case PlayerModel.DamageTypes.None:
                        Debug.Log("Nope!");
                        break;
                }
        }    
    }
}
