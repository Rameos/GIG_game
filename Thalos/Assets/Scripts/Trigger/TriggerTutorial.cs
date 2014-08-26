using UnityEngine;
using System.Collections;
using Controller;

public class TriggerTutorial : MonoBehaviour {

    [SerializeField]
    GameObject tutorialScreen;
    private bool isActive = false;

	void Start () {
        renderer.enabled = false;
        tutorialScreen.SetActive(isActive);
 
	}
	

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Constants.TAG_PLAYER)
        {
            isActive = true;
            Gamestatemanager.OnChangeInGameMenu(Constants.INGAMEMENU_INGAME2DVIEW, true);
            tutorialScreen.SetActive(isActive);
        }
    }
    
    void Update()
    {
        if (Input.GetAxis("ButtonB") > 0.75f && isActive)
        {
            isActive = false;
            Gamestatemanager.OnChangeInGameMenu(Constants.INGAMEMENU_INGAME2DVIEW, false);
            tutorialScreen.SetActive(isActive);
            this.gameObject.SetActive(false);
        }
    }
    
}
