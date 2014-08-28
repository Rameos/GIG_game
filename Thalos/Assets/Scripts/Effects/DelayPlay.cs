using UnityEngine;
using System.Collections;

public class DelayPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(playDelay());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator playDelay()
    {
        yield return new WaitForSeconds(2);
        audio.Play();
    }
}
