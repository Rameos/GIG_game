using UnityEngine;
using System.Collections;
using Backend;
public class BaseReciepeButton : MonoBehaviour {

    Vector3 scaleSelected = Vector3.one * 0.7f;
    Vector3 destinationScale = Vector3.one * 0.6f;
    [SerializeField]
    PlayerModel.PhialType phial;

    
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
    
    
    void EnterFocus()
    {
        destinationScale = scaleSelected;
    }
    void PerformAction()
    {
        switch (phial)
        {
            case PlayerModel.PhialType.Fire:
                GameObject.FindGameObjectWithTag("Player").GetComponent<AlchemySystem>().createPhiole(Recipes.Instance().GetSingleRecipe(Strings.FIREPOTION));
                break;
            
            case PlayerModel.PhialType.Heal:
                GameObject.FindGameObjectWithTag("Player").GetComponent<AlchemySystem>().createPhiole(Recipes.Instance().GetSingleRecipe(Strings.HEALPOTION));
                break;

            case PlayerModel.PhialType.Ice:
                GameObject.FindGameObjectWithTag("Player").GetComponent<AlchemySystem>().createPhiole(Recipes.Instance().GetSingleRecipe(Strings.ICEPOTION));
                break;

        }
    }

    void ExitFocus()
    {
        destinationScale = Vector3.one*0.6f;
    }

    
}
