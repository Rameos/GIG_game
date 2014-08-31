using UnityEngine;
using System.Collections;
using Controller;

public class DelayPlay : MonoBehaviour {

    private float secsToWait = 3;
	// Use this for initialization
	void Start () {
        StartCoroutine(playDelay());
        StartCoroutine(changeToNextlvl());



	}

    IEnumerator playDelay()
    {
        
        yield return new WaitForSeconds(secsToWait);
        audio.Play();
    }

    IEnumerator changeToNextlvl()
    {
        yield return new WaitForSeconds(audio.clip.length);
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
