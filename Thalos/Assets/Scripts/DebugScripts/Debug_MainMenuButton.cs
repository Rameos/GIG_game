using UnityEngine;
using System.Collections;
using Controller;

[ExecuteInEditMode]
public class Debug_MainMenuButton : MonoBehaviour {

    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 150, 150),"StartGame"))
        {
            Debug.Log("StartGame");
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Gamestatemanager>().startGame();
            GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
        }

        else if (GUI.Button(new Rect(20, 180, 150, 150), "Options"))
        {

        }
        else if (GUI.Button(new Rect(20, 350, 150, 150), "EndGame"))
        {

        }
    }
}
