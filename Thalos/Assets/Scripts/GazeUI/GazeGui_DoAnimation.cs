using UnityEngine;
using System.Collections;

namespace GazeGUI
{
    public class GazeGui_DoAnimation : BaseGazeUI
    {

        
        private Vector3 destinationScale = Vector3.one;
        public override void OnEventStart()
        {
            Debug.Log("fancyDancy");
            
        }

        public override void OnGazeEnter()
        {
            destinationScale *= 1.5f;
        }

        public override void OnGazeStay()
        {

        }

        public override void OnGazeExit()
        {
            destinationScale = Vector3.one;
            Debug.Log("Exit");
        }

        void Update()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, destinationScale, 0.01f);
        }

    }
}