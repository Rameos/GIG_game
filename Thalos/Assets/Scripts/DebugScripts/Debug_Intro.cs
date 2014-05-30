using UnityEngine;
using System.Collections;
using Controller;

public class Debug_Intro : MonoBehaviour {

	void Start () {
        StartCoroutine(waitForNextstage());
	}

    IEnumerator waitForNextstage()
    {
        yield return new WaitForSeconds(4);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<Gamestatemanager>().loadLevel(2);
    }
}
