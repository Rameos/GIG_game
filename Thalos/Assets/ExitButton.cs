using UnityEngine;
using System.Collections;

public class ExitButton : BaseMainMenuButton {

    void OnMouseDown()
    {
        Application.Quit();
    }

}
