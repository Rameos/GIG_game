using UnityEngine;
using System.Collections;
using Controller;
using GazeGUI;
public class AlchemyGUIManager : MonoBehaviour {

    private bool isPopUpWindowOpen = false;
    [SerializeField]
    private GameObject[] popupWindow;
    private bool isSelectable = true;

    private bool isGazeActive = false;

    [SerializeField]
    private BaseReciepeButton[] recipes;
    [SerializeField]
    private float delay;


    [SerializeField]
    private float threseholdController = 0.1f;
    private bool canSwitchBetweenItems = true;
    private int IDSelection = 0;

    private bool isMainMenuActive = true;


	void Start () {
	
        
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("ButtonB") > 0.75f)
        {
            if(isSelectable)
            {
                Debug.Log("ButtonB pressed");
                StartCoroutine(waitForInput());
            }
        }

        if(!isGazeActive)
        {
            manageInputForMainMenu();
        }
	}


    private void manageInputForMainMenu()
    {
        float input = Input.GetAxis("Vertical");

        if (Mathf.Abs(input) > threseholdController)
        {
            if (canSwitchBetweenItems)
            {
                StartCoroutine(changeItemSelection(input));
            }
        }

        else if (Input.GetAxis("ButtonA") > 0)
        {
            Debug.Log("ButtonA");
            recipes[IDSelection].SelectItem();
        }
    }

    public void createPopUp(int MessageID)
    {
        changeGlobalStatusOfPopup(false); 
        popupWindow[MessageID].SetActive(true);
        isPopUpWindowOpen = true;
    }

    IEnumerator waitForInput()
    {
        isSelectable = false;
        Debug.Log("Close/Backbutton Pressed");

        if (isPopUpWindowOpen)
        {
            isPopUpWindowOpen = false;
            changeGlobalStatusOfPopup(false);
        }
        else
        {
            Gamestatemanager.OnChangeInGameMenu(Constants.INGAMEMENU_INVENTORY, false);
            isPopUpWindowOpen = false;
        }
        
        yield return new WaitForSeconds(0.5f);

        isSelectable = true;
    }
    
    void OnEnable()
    {
        isSelectable = true;
        changeGlobalStatusOfPopup(false);
    }
    
    private void changeGlobalStatusOfPopup(bool change)
    {
        foreach (GameObject e in popupWindow)
            {
                e.SetActive(change);
            }
    }

    IEnumerator changeItemSelection(float input)
    {
        canSwitchBetweenItems = false;

        recipes[IDSelection].DeselectItem();
        int nextIDStep = 0;

        if (input > 0)
        {
            nextIDStep = -1;
        }
        else
        {
            nextIDStep = 1;
        }

        if (IDSelection + nextIDStep >= recipes.Length)
        {
            Debug.Log("CHANGE BACK!");
            nextIDStep = 0;
            IDSelection = nextIDStep;

            recipes[nextIDStep].SelectItem();

        }
        else if (IDSelection + nextIDStep < 0)
        {
            Debug.Log("Smaller than Zero!");
            nextIDStep = recipes.Length - 1;
            IDSelection = nextIDStep;

            recipes[nextIDStep].SelectItem();
        }
        else
        {
            recipes[IDSelection + nextIDStep].SelectItem();
            IDSelection += nextIDStep;
        }

        yield return new WaitForSeconds(0.5f);
        canSwitchBetweenItems = true;

    }

}