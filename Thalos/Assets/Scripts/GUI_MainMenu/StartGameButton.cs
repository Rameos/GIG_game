using UnityEngine;
using System.Collections;
using Controller;
public class StartGameButton : BaseMainMenuButton {

    void OnMouseDown()
    {
        DoActionWhenActivated();
    }

    public override void DoActionWhenActivated()
    {

        GameObject.FindGameObjectWithTag(Constants.TAG_GAMEMANAGER).GetComponent<Gamestatemanager>().startGame();
        GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
    }
}
