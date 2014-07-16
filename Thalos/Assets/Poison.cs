using UnityEngine;
using System.Collections;

public class Poison : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision information)
    {
        Debug.Log("Boom");
        StartCoroutine(explosion());
    }

    IEnumerator explosion()
    {
        yield return new WaitForSeconds(0.1f);
        
    }

}
