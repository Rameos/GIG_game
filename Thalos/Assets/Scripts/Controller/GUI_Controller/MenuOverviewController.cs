using UnityEngine;
using System.Collections;
using iViewX;
using Controller;

public class MenuOverviewController : MonoBehaviour {

	// public Texture3D blubb;
	// private int toolbarGridInt = 0; 
	// private string[] toolbarStrings = {"Inventory", "Mixing"}; 
	
	void OnGUI() {

		// toolbarGridInt = GUI.Toolbar (new Rect (25, 30, 100, 40), toolbarGridInt, toolbarStrings); 

		// if (GUI.changed) 
		// {
		// if(0 == toolbarGridInt) 
		// {
		// print("Inventory selected");
				//Application.LoadLevel(5); 
		// GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
		// } 
		// else 
		// {
		// print ("Mixing selected"); 
				//Application.LoadLevel(4); 
		// GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
		// }
		// }

		// GUI.BeginGroup (new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 500, 500)); 		
		GUI.Box (new Rect (Screen.width / 2 - 250, Screen.height / 2 - 250, 650, 500), "Overview");		
		if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2, 150, 150), "Inventory")) {
			// Application.LoadLevel(5); 
			print ("Load menu for inventory");
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
		}
		else if (GUI.Button(new Rect(Screen.width / 2 + 100,Screen.height / 2,150,150), "Mixing")) {
			// Application.LoadLevel(4);
			print ("Load Menu for mixing");
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
		}
		// GUI.EndGroup (); 
	}
}