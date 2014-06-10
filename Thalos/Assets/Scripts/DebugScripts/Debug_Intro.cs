using UnityEngine;
using System.Collections;
using Controller;

public class Debug_Intro : MonoBehaviour {

	void Start () {
        StartCoroutine(waitForNextstage());
	}

    IEnumerator waitForNextstage()
    {
        Debug.Log("StartCoroutine");
        yield return new WaitForSeconds(4);
        Debug.Log("StartCoroutine");
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<Gamestatemanager>().loadLevel(2);
    }
}
