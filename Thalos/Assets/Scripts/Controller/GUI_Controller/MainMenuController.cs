using UnityEngine;
using System.Collections;
using iViewX;
using Controller;

public class MainMenuController : MonoBehaviour {

	void OnGUI() {

		//Make a background box
		GUI.BeginGroup (new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50,  Screen.width, Screen.height)); 

		GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2), "Loader Menu");

		if (GUI.Button (new Rect (20, 40, 80, 20), "Main Menu")) {
			Application.LoadLevel(1); 
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
			}

		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Starte Demoversion")) {
			Application.LoadLevel(2);
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
		}

		if(GUI.Button(new Rect(20,90,80,20), "Inventory")) {
			//Application.LoadLevel(2);
			print("Inventory clicked"); 
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
		}

		if(GUI.Button(new Rect(20,120,80,20), "Mixing")) {
			//Application.LoadLevel(2);
			print ("Mixing clicked");
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
		}
		
		GUI.EndGroup (); 
	}
}
