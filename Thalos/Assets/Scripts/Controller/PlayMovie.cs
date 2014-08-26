using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

    [SerializeField]
    private MovieTexture movie;

    private Material movieMaterial;
    private Color32 destinationColor;
    private Color32 transparentColor = new Color32(0,0,0,0);
    private Color32 finalColor = new Color32(255,255,255,255);
	// Use this for initialization
	void Start () {
        movieMaterial = renderer.material;
        destinationColor = finalColor;
        movieMaterial.color = transparentColor;
        
	}
	
	// Update is called once per frame
	void Update () {

        movieMaterial.color = Color32.Lerp(movieMaterial.color, destinationColor, 0.1f);

        if(movieMaterial.color.a >=0.95f && !movie.isPlaying)
        {
            Debug.Log("StartMovie!");
            startMovie();
        }
	}

    private void startMovie()
    {
        renderer.material.mainTexture = movie;
        movie.Play();
        StartCoroutine(waitToEndOfSequence());
    }

    IEnumerator waitToEndOfSequence()
    {
        yield return new WaitForSeconds(movie.duration);
        Application.LoadLevel(Application.loadedLevel+1);
    }
}
