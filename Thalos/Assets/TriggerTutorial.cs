using UnityEngine;
using System.Collections;
using Controller;

public class TriggerTutorial : MonoBehaviour {



	void Start () {
        renderer.enabled = false;
 
	}
	

    void OnTriggerEnter(Collider other)
    {
        Gamestatemanager.OnChangeInGameMenu(Constants.INGAMEMENU_INGAME2DVIEW,true);
        
    }
    
    void Update()
    {
        if (Input.GetAxis("ButtonB") > 0.75f)
        {
            Gamestatemanager.OnChangeInGameMenu(Constants.INGAMEMENU_INGAME2DVIEW, false);
        }
    }
    
}
