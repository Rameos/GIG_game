using UnityEngine;
using System.Collections;
using iViewX;
using Controller;

public class MenuOverviewController : MonoBehaviour {

	//public Texture3D blubb;
	private int selectionGridInt = 0; 
	private string[] selectionStrings = {"Inventory", "Mixing"}; 
	
	void OnGUI() {

		selectionGridInt = GUI.SelectionGrid (new Rect (25, 25, 100, 30), selectionGridInt, selectionStrings, 1); 

		if (GUI.changed) 
		{
			if(0 == selectionGridInt) 
			{
				print("Inventory selected");
			} 
			else 
			{
				print ("Mixing selected"); 
			}
		}

		// GUI.BeginGroup (new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 300, 300)); 		
		// GUI.Box (new Rect (0, 0, 300, 300), "Overview");		
		// if (GUI.Button (new Rect (20, 40, 80, 20), "Inventory")) {
			// Application.LoadLevel(1); 
			// Inventory laden
		//	print ("Load menu for inventory");
		// }
		// if (GUI.Button(new Rect(20,70,80,20), "Mixing")) {
			// Application.LoadLevel(2);
			// RingMenuController laden
		//	print ("Load Menu for mixing (RingMenuController)");
		// }
		// GUI.EndGroup (); 
	}
}