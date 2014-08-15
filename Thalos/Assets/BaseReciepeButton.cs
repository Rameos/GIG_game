using UnityEngine;
using System.Collections;
using Backend;
using Controller;
public class BaseReciepeButton : MonoBehaviour {

    Vector3 scaleSelected = Vector3.one * 0.7f;
    Vector3 destinationScale = Vector3.one * 0.6f;
    [SerializeField]
    PlayerModel.PhialType phial;

    private bool isSelected = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.localScale = Vector3.Slerp(transform.localScale, destinationScale, 0.2f);


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
    
    void getInfo()
    {
        switch (phial)
        {
            case PlayerModel.PhialType.Fire:
                GameObject.FindGameObjectWithTag("AlchemySystemMenu").GetComponent<AlchemyGUIManager>().createPopUp(Constants.POPUP_ID_FIREHELP);
                break;

            case PlayerModel.PhialType.Heal:
                GameObject.FindGameObjectWithTag("AlchemySystemMenu").GetComponent<AlchemyGUIManager>().createPopUp(Constants.POPUP_ID_HEALHELD);
                break;

            case PlayerModel.PhialType.Ice:
                GameObject.FindGameObjectWithTag("AlchemySystemMenu").GetComponent<AlchemyGUIManager>().createPopUp(Constants.POPUP_ID_ICE);
                break;

        }
    }
    
    void EnterFocus()
    {
        destinationScale = scaleSelected;
    }
    void PerformAction()
    {
        bool isDone = true;
        switch (phial)
        {
            case PlayerModel.PhialType.Fire:
               isDone= GameObject.FindGameObjectWithTag("Player").GetComponent<AlchemySystem>().createPhiole(Recipes.Instance().GetSingleRecipe(Strings.FIREPOTION));
                break;
            
            case PlayerModel.PhialType.Heal:
                isDone= GameObject.FindGameObjectWithTag("Player").GetComponent<AlchemySystem>().createPhiole(Recipes.Instance().GetSingleRecipe(Strings.HEALPOTION));
                break;

            case PlayerModel.PhialType.Ice:
                isDone= GameObject.FindGameObjectWithTag("Player").GetComponent<AlchemySystem>().createPhiole(Recipes.Instance().GetSingleRecipe(Strings.ICEPOTION));
                break;

        }

        if(isDone == false)
        {
            GameObject.FindGameObjectWithTag("AlchemySystemMenu").GetComponent<AlchemyGUIManager>().createPopUp(Constants.POPUP_ID_NORESOURCES);
        }
    }

    void ExitFocus()
    {
        destinationScale = Vector3.one*0.6f;
    }

    
}
