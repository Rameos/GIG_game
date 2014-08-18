using UnityEngine;
using System.Collections;
using Controller;

public class TriggerTutorial : MonoBehaviour {

    [SerializeField]
    GameObject tutorialScreen;

	void Start () {
        renderer.enabled = false;
        tutorialScreen.SetActive(false);
 
	}
	

    void OnTriggerEnter(Collider other)
    {
        Gamestatemanager.OnChangeInGameMenu(Constants.INGAMEMENU_INGAME2DVIEW,true);

        tutorialScreen.SetActive(true);
    }
    
    void Update()
    {
        if (Input.GetAxis("ButtonB") > 0.75f)
        {
            Gamestatemanager.OnChangeInGameMenu(Constants.INGAMEMENU_INGAME2DVIEW, false);
            tutorialScreen.SetActive(false);
        }
    }
    
}
