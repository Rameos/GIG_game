using UnityEngine;
using System.Collections;
using GazeGUI;

public class NoGazeButton_Reciepe :BaseMainMenuButton  {

    

    public override void DoActionWhenActivated()
    {
        gameObject.GetComponent<BaseReciepeButton>().PerformAction();
    }
}
