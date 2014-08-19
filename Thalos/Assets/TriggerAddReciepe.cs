using UnityEngine;
using System.Collections;
using Controller;
using Backend;

public class TriggerAddReciepe : MonoBehaviour {

    [SerializeField]
    GameObject tutorialScreen;
    
    [SerializeField]
    PlayerModel.PhialType type;

    private bool isActive = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.TAG_PLAYER)
        {
            isActive = true;
            Gamestatemanager.OnChangeInGameMenu(Constants.INGAMEMENU_INGAME2DVIEW, true);
            PlayerModel.Instance().addRecipe(PlayerModel.getRecipe(type));

        }
    }


    void Update()
    {
        if (Input.GetAxis("ButtonB") > 0.75f && isActive)
        {
            isActive = false;
            Gamestatemanager.OnChangeInGameMenu(Constants.INGAMEMENU_INGAME2DVIEW, false);
            //tutorialScreen.SetActive(isActive);
            this.gameObject.SetActive(false);
        }
    }

}
