using UnityEngine;
using System.Collections;
using Controller;

public class PauseMenu_RestartGame : BaseMainMenuButton {

    public override void DoActionWhenActivated()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<Gamestatemanager>().loadLevel(Application.loadedLevel);
    }
}
