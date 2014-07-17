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

        private bool isShotable = true;
        private bool isThrowable = true;

        public void Shot()
        {
            if(isShotable)
            {
                StartCoroutine(shotCoolDown());
            }            
        }

        public void ThrowPoison()
        {
            if(isThrowable)
            {
                StartCoroutine(throwCoolDown());
            }
        }

        IEnumerator shotCoolDown()
        {
            PlayerModel.DamageTypes damage = PlayerModel.Instance().DamageType_Bolt;
            GameObject instance;
            switch (damage)
            {
                case PlayerModel.DamageTypes.Fire:
                    instance = GameObject.Instantiate(BoltFire, instanciatePointBullet.position, instanciatePointBullet.localRotation) as GameObject;
                    instance.GetComponent<Bullet>().Init(transform.forward, Constants.ID_PLAYER, new Damage(Constants.damageFireBolt, PlayerModel.DamageTypes.Fire));
                    break;

                case PlayerModel.DamageTypes.Ice:
                    instance = GameObject.Instantiate(BoltIce, instanciatePointBullet.position, instanciatePointBullet.localRotation) as GameObject;
                    instance.GetComponent<Bullet>().Init(transform.forward, Constants.ID_PLAYER, new Damage(Constants.damageIceBolt, PlayerModel.DamageTypes.Ice));
                    break;

                case PlayerModel.DamageTypes.Standard:
                    instance = GameObject.Instantiate(BoltNormal, instanciatePointBullet.position, instanciatePointBullet.localRotation) as GameObject;
                    instance.GetComponent<Bullet>().Init(transform.forward, Constants.ID_PLAYER, new Damage(Constants.damageStandardBolt, PlayerModel.DamageTypes.Standard));

                    break;
            }
            isShotable = false; 
            yield return new WaitForSeconds(Constants.COOLDOWN_BOLT);
            isShotable = true; 
        }

        IEnumerator throwCoolDown()
        {
            PlayerModel.DamageTypes damage = PlayerModel.Instance().DamageType_Poision;
            GameObject instance;
            switch (damage)
            {
                case PlayerModel.DamageTypes.Fire:
                    instance = GameObject.Instantiate(PoisonFire, instanciatePointPoison.position, instanciatePointPoison.localRotation) as GameObject;
                    instance.GetComponent<Poison>().Init(transform.forward,new Damage(Constants.damageIcePoison, PlayerModel.DamageTypes.Fire),Constants.ID_PLAYER);
                    
                    break;

                case PlayerModel.DamageTypes.Ice:
                    instance = GameObject.Instantiate(PoisonIce, instanciatePointPoison.position, instanciatePointPoison.localRotation) as GameObject;
                    instance.GetComponent<Poison>().Init(transform.forward, new Damage(Constants.damageIceBolt, PlayerModel.DamageTypes.Ice), Constants.ID_PLAYER);
                    break;
            }
            Debug.Log("DamageTypePoison: " + damage);

            isThrowable = false;
            yield return new WaitForSeconds(Constants.COOLDOWN_POISON);
            isThrowable = true; 
        }
    
    }
}
