using UnityEngine;
using System.Collections;
using Controller;
public class StartGameButton : BaseMainMenuButton {

    void OnMouseDown()
    {

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<Gamestatemanager>().startGame();
        GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
    }
}
