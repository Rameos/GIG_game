using UnityEngine;
using System.Collections;

public class intro_ShowNewspaper : MonoBehaviour {

    public float waitForFadeIn = 2f;
    public float visibilityTime = 5f;
    
    private bool isFadeActive = false;
    private Color32 destinationColor;
	
    void Start () {

        StartCoroutine(waitForstartFading());
        renderer.material.color = new Color32(0, 0, 0, 0);
        destinationColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () {

        if(isFadeActive)
        {
            renderer.material.color = Color.Lerp(renderer.material.color, destinationColor, 0.01f);
            
            if(renderer.material.color.a <=0.001f)
            {
                renderer.enabled = false; 
            }
        }

	}

    IEnumerator waitForstartFading()
    {
        destinationColor = Color.white;
        yield return new WaitForSeconds(waitForFadeIn);
        Debug.Log("StartFade");
        
        isFadeActive = true;
        StartCoroutine(waitForVisibilityTime());
    }

    IEnumerator waitForVisibilityTime()
    {
        yield return new WaitForSeconds(visibilityTime);
        destinationColor = new Color32(0, 0, 0, 0);
    }
}
