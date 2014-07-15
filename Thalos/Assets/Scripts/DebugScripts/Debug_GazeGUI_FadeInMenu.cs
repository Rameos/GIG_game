using UnityEngine;
using System.Collections;

public class Debug_GazeGUI_FadeInMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animation.Play("CircleMenuIN");
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animation.Play("CircleMenuOUT");
        }
	}
}
