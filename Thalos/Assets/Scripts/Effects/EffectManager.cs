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

        public static startFirePoisonEffect StartFirePoisonEffectHandler;
        public static startFirePoisonEffect StartIcePoisonEffectHandler;



        public static void OnStartFirePoisonEffect(Vector3 triggerPosition)
        {
            if (StartFirePoisonEffectHandler != null)
            {
                StartFirePoisonEffectHandler(triggerPosition);
            }
        }

        public static void OnStartIcePoisonEffect(Vector3 triggerPosition)
        {
            if(StartIcePoisonEffectHandler != null)
            {
                StartIcePoisonEffectHandler(triggerPosition);
            }

        }

    }
}
