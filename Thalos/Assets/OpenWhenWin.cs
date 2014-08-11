using UnityEngine;
using System.Collections;

public class OpenWhenWin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Winning.isDone)
        {
            renderer.enabled = true;
        }
	}
}
