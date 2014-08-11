
using UnityEngine;
using System.Collections;
using Controller;

[ExecuteInEditMode]
public class Debug_MainMenuButton : MonoBehaviour {

    [SerializeField]
    private float posX = 600;

    [SerializeField]
    private float posY = 150;
    void OnGUI()
    {
        if (GUI.Button(new Rect(posX, posY, 150, 150), "StartGame"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Gamestatemanager>().startGame();
            GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
        }

        else if (GUI.Button(new Rect(posX, posY+150, 150, 150), "Options"))
        {
            GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
        }

        else if (GUI.Button(new Rect(posX, posY+300, 150, 150), "EndGame"))
        {
            GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Gamestatemanager>().endGame();
        }
    }
}
