using UnityEngine;
using System.Collections;
using iViewX;

public class StartCalibrationButton : BaseMainMenuButton  {


    public override void DoActionWhenActivated()
    {
        GazeControlComponent.Instance.StartCalibration(5);
    
    }
}
