using UnityEngine;
using System.Collections;
using Controller;
public class DestroyAfterTimer : MonoBehaviour {

    public float lifeTime = 2; 

	// Use this for initialization
	void Start () {

        StartCoroutine(destroyAfterWait());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator destroyAfterWait()
    {
        if (gameObject.particleEmitter != null)
        {
            yield return new WaitForSeconds(0.1f);
            particleEmitter.emit = false;
        
        }
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
