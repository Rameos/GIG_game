using UnityEngine;
using System.Collections;
using iViewX;
using Controller;

namespace DebugItems
{
    public class MenuOverviewController : MonoBehaviour
    {

        // public Texture3D blubb;
        // private int toolbarGridInt = 0; 
        // private string[] toolbarStrings = {"Inventory", "Mixing"}; 

        void OnGUI()
        {

            GUI.Box(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 650, 500), "Overview");
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 150, 150), "Inventory"))
            {
                // Application.LoadLevel(5); 
                print("Load menu for inventory");
                GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
            }
            else if (GUI.Button(new Rect(Screen.width / 2 + 100, Screen.height / 2, 150, 150), "Mixing"))
            {
                // Application.LoadLevel(4);
                print("Load Menu for mixing");
                GameObject.FindGameObjectWithTag("UISoundManager").GetComponent<UISoundManager>().playClickSound();
            }
        }
    }
}
