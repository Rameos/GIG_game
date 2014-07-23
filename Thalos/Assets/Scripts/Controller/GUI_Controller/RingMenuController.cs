using UnityEngine;
using System.Collections;
using iViewX;
using Controller;

public class RingMenuController : MonoBehaviour {

	void OnGUI() {

		//Make a background box
		GUI.Box (new Rect (10, 10, 100, 90), "Loader Menu"); 

		//Make the first button. If it is pressed, Application.LoadLevel will be executed
		if (GUI.Button (new Rect (20, 40, 80, 20), "Level 1")) {
			Application.LoadLevel(1); 
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
			}

		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Level 2")) {
			Application.LoadLevel(2);
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
		}
	}
}
