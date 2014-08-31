using UnityEngine;
using System.Collections;
using Controller;

namespace Controller
{
    public class BlurrManager : MonoBehaviour
    {
        [SerializeField]
        Blur blur;

        private bool isBlur;
        private float blurfactor = 0.1f;
        private float destinationFloat = 0;
        private float maxBlur = 10f;

        #region UnityFunction
        void Start()
        {

            blur = Camera.main.GetComponent<Blur>();
            Gamestatemanager.ChangeInGameMenuHandler += Gamestatemanager_ChangeInGameMenuHandler;

            blur.enabled = false;
        }

        void Update()
        {

            if (isBlur)
            {

                blur.enabled = true;
                blur.blurSize = Mathf.Lerp(blur.blurSize, destinationFloat, 0.3f);

                if (blur.blurSize == destinationFloat || blur.blurSize <= 0.001f)
                {
                    isBlur = false;
                    blur.enabled = false;
                }
            }
        }
        #endregion

        void Gamestatemanager_ChangeInGameMenuHandler(int ID_Menu, bool status)
        {

            Debug.Log("Open any Menu");
            isBlur = true;


            if (status)
            {
                destinationFloat = maxBlur;
            }
            else
            {
                destinationFloat = 0;
                blur.blurSize = 0;
                blur.enabled = false;

            }

        }



    }

}
