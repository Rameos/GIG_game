using UnityEngine;
using System.Collections;
using Controller;
public class StopIntro : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("ButtonStart"))
            {
                try
                {
                    Gamestatemanager manager = GameObject.FindGameObjectWithTag(Constants.TAG_GAMEMANAGER).GetComponent<Gamestatemanager>();
                    manager.loadLevel(Application.loadedLevel + 1);
                }
                catch
                {
                    Debug.Log("Error"); 
                }

            }

	}
}
