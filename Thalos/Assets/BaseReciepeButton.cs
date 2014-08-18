using UnityEngine;
using System.Collections;
using Backend;
using Controller;

namespace GazeGUI
{
    public class BaseReciepeButton : BaseGazeUI {

        Vector3 scaleSelected = Vector3.one * 0.7f;
        Vector3 destinationScale = Vector3.one * 0.6f;
        [SerializeField]
        PlayerModel.PhialType phial;

        private AlchemyGUIManager guiManager;
        private bool isSelectable = false;
        private bool buttonDown = false;

        private bool performedAction = false;

	    void Start () {
            guiManager = GameObject.FindGameObjectWithTag("AlchemySystemMenu").GetComponent<AlchemyGUIManager>();
	    }

	    void Update () {

            transform.localScale = Vector3.Slerp(transform.localScale, destinationScale, 0.2f);

            if(Input.GetAxis("ButtonA")>0.75f)
            {
                if(!buttonDown)
                {
                    buttonDown = true;
                    StartCoroutine(waitForUpdate());
                }
                
            }

            else if (Input.GetAxis("ButtonX")>0.75f)
            {
                if (!buttonDown)
                {
                    buttonDown = true;
                    StartCoroutine(waitForInfo());
                }
            }
	    }
    
        void OnMouseOver()
        {
            EnterFocus();
        }

        void OnMouseExit()
        {
            ExitFocus();
        }

        void OnMouseDown()
        {
            Debug.Log("MouseDownItem");
            PerformAction();
        }
    
        void GetInfo()
        {
            if(isSelectable)
            {
                Debug.Log("Phial:" + phial);

                switch (phial)
                {
                    case PlayerModel.PhialType.Fire:
                        guiManager.createPopUp(Constants.POPUP_ID_FIREHELP);
                        break;

                    case PlayerModel.PhialType.Heal:
                        guiManager.createPopUp(Constants.POPUP_ID_HEALHELP);
                        break;

                    case PlayerModel.PhialType.Ice:
                        guiManager.createPopUp(Constants.POPUP_ID_ICE);
                        break;
                }
            }
        }
    
        void EnterFocus()
        {
            isSelectable = true;
            destinationScale = scaleSelected;
        }
    
        void PerformAction()
        {
            if (isSelectable)
            {
                bool isDone = true;

                switch (phial)
                {
                    case PlayerModel.PhialType.Fire:
                        isDone = GameObject.FindGameObjectWithTag("Player").GetComponent<AlchemySystem>().createPhiole(Recipes.Instance().GetSingleRecipe(Strings.FIREPOTION));
                        break;

                    case PlayerModel.PhialType.Heal:
                        isDone = GameObject.FindGameObjectWithTag("Player").GetComponent<AlchemySystem>().createPhiole(Recipes.Instance().GetSingleRecipe(Strings.HEALPOTION));
                        break;

                    case PlayerModel.PhialType.Ice:
                        isDone = GameObject.FindGameObjectWithTag("Player").GetComponent<AlchemySystem>().createPhiole(Recipes.Instance().GetSingleRecipe(Strings.ICEPOTION));
                        break;
                }

                if (isDone == false)
                {
                    GameObject.FindGameObjectWithTag("AlchemySystemMenu").GetComponent<AlchemyGUIManager>().createPopUp(Constants.POPUP_ID_NORESOURCES);
                }

                else
                {
                    Debug.Log("Play Sound!!");
                }
            }

        }

        void ExitFocus()
        {
            isSelectable = false;
            destinationScale = Vector3.one*0.6f;
        }
        
        public override void OnGazeEnter()
        {
            EnterFocus();
        }

        public override void OnGazeStay()
        {
            //Banana
        }

        public override void OnGazeExit()
        {
            ExitFocus();
        }

        public override void OnEventStart()
        {
            PerformAction();
        }

        IEnumerator waitForUpdate()
        {
            PerformAction();
            yield return new WaitForSeconds(0.5f);
            buttonDown = false;
        }

        IEnumerator waitForInfo()
        {
            GetInfo();
            yield return new WaitForSeconds(0.5f);
            buttonDown = false;
        }

    }
}
