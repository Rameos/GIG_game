using UnityEngine;
using System.Collections;
using iViewX;
using Controller;

public class MainMenuController : MonoBehaviour {

	void OnGUI() {

		GUI.BeginGroup (new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50,  Screen.width, Screen.height)); 

		GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2), "Loader Menu");

		if (GUI.Button (new Rect (20, 50, 100, 30), "Main Menu")) 
		{
			Application.LoadLevel(1); 
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
		}

		else if(GUI.Button(new Rect(20,100,100,30), "Start Game"))
		{
			Application.LoadLevel(2);
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
		}

		else if(GUI.Button(new Rect(20,150,100,30), "MenuOverview")) 
		{
			//Application.LoadLevel(3);
			print ("Mixing clicked");
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
		}

		else if(GUI.Button(new Rect(20,200,100,30), "Mixing")) 
		{
			//Application.LoadLevel(4);
			print ("Mixing clicked");
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
		}

		else if(GUI.Button(new Rect(20,250,100,30), "Inventory")) 
		{
			//Application.LoadLevel(5);
			print("Inventory clicked"); 
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
		}

		else if (GUI.Button(new Rect(20, 350, 150, 150), "End Game"))
		{
			GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<Gamestatemanager>().endGame();
		}
		
		GUI.EndGroup (); 
	}
}
