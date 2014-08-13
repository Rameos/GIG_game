using UnityEngine;
using System.Collections;

public class ExitButton : BaseMainMenuButton {

    void OnMouseDown()
    {
        DoActionWhenActivated();
    }


    public override void DoActionWhenActivated()
    {
        Application.Quit();
    }
}
