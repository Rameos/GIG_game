using UnityEngine;
using System.Collections;
using iViewX;
using Controller;

public class MainMenuController : MonoBehaviour {

    private BaseMainMenuButton[] buttonsMainmenu;
    public Color32 colorText { get; set; }
    public Color32 colorSprites { get; set; }

    [SerializeField]
    private float delay;


    [SerializeField]
    private float threseholdController = 0.1f;
    private bool isFadeActive = false;
    private bool canSwitchBetweenItems = true;
    private int IDSelection = 0; 
    void Start()
    {
        buttonsMainmenu = GetComponentsInChildren<BaseMainMenuButton>();


        Debug.Log("Count: " + buttonsMainmenu.Length);
        buttonsMainmenu[IDSelection].SelectItem(); 
    }

    void Update()
    {
        manageInput();
    }

    private void manageInput()
    {
        float input = Input.GetAxis("Vertical");
        if (Mathf.Abs(input) > threseholdController)
        {
            if(canSwitchBetweenItems)
            {
                StartCoroutine(changeItemSelection(input));
            }
            
        }

        else if (Input.GetAxis("ButtonA") > 0)
        {
            Debug.Log("ButtonA");
            buttonsMainmenu[IDSelection].DoActionItem();
        }
    }


    IEnumerator changeItemSelection(float input)
    {
        canSwitchBetweenItems = false;
        Debug.Log("input :" + input);
        Debug.Log("ID:" + IDSelection);
        
        
        
        buttonsMainmenu[IDSelection].DeselectItem();
        int nextIDStep =0;
        
        
        
        if(input>0)
        {
            nextIDStep=-1;
        }
        else
        {
            nextIDStep=1;
        }
        
        
        if(IDSelection+nextIDStep>=buttonsMainmenu.Length)
        {
            Debug.Log("CHANGE BACK!");
            nextIDStep = 0;
            IDSelection = nextIDStep;
       
            buttonsMainmenu[nextIDStep].SelectItem();

        }
        else if(IDSelection+nextIDStep<0)
        {
            Debug.Log("Smaller than Zero!");
            nextIDStep = buttonsMainmenu.Length-1;
            IDSelection = nextIDStep;

            buttonsMainmenu[nextIDStep].SelectItem();
        }
        else
        {
            buttonsMainmenu[IDSelection+nextIDStep].SelectItem();
            IDSelection += nextIDStep;

        }

        

        yield return new WaitForSeconds(0.5f);
        canSwitchBetweenItems = true;
        
    }

}
