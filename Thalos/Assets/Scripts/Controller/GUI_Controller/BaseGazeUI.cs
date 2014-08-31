using UnityEngine;
using System.Collections;

namespace GazeUI
{
    public abstract class BaseGazeUI : MonoBehaviour
    {
        private bool isSelected = false; 

        public void onObjectHit()
        {
            if (isSelected)
            {
                OnGazeStay();
            }
            else
            {
                OnGazeEnter();
                isSelected = true;
            }
        }

        public void OnObjectExit()
        {
            isSelected = false;
            OnGazeExit();
        }

        public abstract void OnGazeEnter();

        public abstract void OnGazeStay();

        public abstract void OnGazeExit();

        public abstract void OnEventStart();
    }   
}