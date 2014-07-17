using UnityEngine;
using System.Collections;

namespace Effects
{

    public delegate void startFirePoisonEffect(Vector3 triggerPosition);
    public delegate void startIcePoisonEffect(Vector3 triggerPosition);

    public class EffectManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject effectFirePoison;

        [SerializeField]
        private GameObject effectIcePoision;


        private static EffectManager instance;

        public static EffectManager Instance()
        {
            if(instance!= null)
            {
                instance = new EffectManager(); 
            }

            return instance; 
        }

        public void OnStartIcePoisonEffect(Vector3 position)
        {
            GameObject instance = GameObject.Instantiate(effectFirePoison, position, effectFirePoison.transform.localRotation) as GameObject;
        }

        public void OnStartFirePoisonEffect(Vector3 position)
        {
            GameObject instance = GameObject.Instantiate(effectIcePoision, position, effectFirePoison.transform.localRotation) as GameObject;
        }
    }
}
