using UnityEngine;
using System.Collections;
using Controller;
public class AlchemyGUIManager : MonoBehaviour {

    private bool isPopUpWindowOpen = false;
    [SerializeField]
    private GameObject[] popupWindow;
    private bool isSelectable = true; 

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
}
