using UnityEngine;
using System.Collections;

public class AlchemyGUIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("ButtonB") > 0)
        {
            Debug.Log("Close/Backbutton Pressed");
        }
	}
}
