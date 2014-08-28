using UnityEngine;
using System.Collections;

namespace Controller
{
    public class TimeManager : MonoBehaviour
    {
        private static TimeManager instance;
        private float deltaTime; 
        void Start()
        {
            deltaTime = Time.fixedDeltaTime;
            Gamestatemanager.ChangeInGameMenuHandler += manageTimeScale;
        }

        public TimeManager Instance()
        {
            if(instance == null)
            {
                instance = new TimeManager(); 
            }

            return instance;
        }


        public void manageTimeScale(int levelID, bool status)
        {
            Debug.Log("ManageTimeScale");

            if(status == true)
            {
                if (levelID == Constants.INGAMEMENU_INVENTORY || levelID == Constants.INGAMEMENU_PAUSE)
                {
                    Debug.Log("Stop Game!");
                    Time.timeScale = 0f;
                }
            }
            else if(status == false)
            {

                Debug.Log("Resume Game!");
                Time.timeScale = 1;
            }
        }

    }
}
