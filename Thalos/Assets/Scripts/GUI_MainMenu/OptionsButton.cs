using UnityEngine;
using System.Collections;

public class OptionsButton : BaseMainMenuButton {

    void OnMouseDown()
    {
        DoActionWhenActivated();
    }
    
    
    public override void DoActionWhenActivated()
    {
        throw new System.NotImplementedException();
    }
}
