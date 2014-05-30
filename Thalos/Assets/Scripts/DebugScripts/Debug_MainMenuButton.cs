using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Debug_MainMenuButton : MonoBehaviour {

    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 150, 150),"StartGame"))
        {

        }

        else if (GUI.Button(new Rect(20, 180, 150, 150), "Options"))
        {

        }
        else if (GUI.Button(new Rect(20, 350, 150, 150), "EndGame"))
        {

        }
    }
}
