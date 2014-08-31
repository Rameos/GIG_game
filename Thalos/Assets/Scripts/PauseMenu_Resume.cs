using UnityEngine;
using System.Collections;
using Controller;

public class PauseMenu_Resume :  BaseMainMenuButton{

    void OnMouseDown()
    {
        DoActionWhenActivated();
    }


    public override void DoActionWhenActivated()
    {
        Gamestatemanager.OnChangeInGameMenu(Constants.INGAMEMENU_PAUSE, false);
    }
}
