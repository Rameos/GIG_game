using UnityEngine;
using System.Collections;

public class FadeSceneEffect : MonoBehaviour {

    private float fadeSpeed = 1.5f;
    private bool sceneStarting = true;

    void Awake()
    {
        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }

    void Update()
    {
        if (sceneStarting)
            FadeToClear();
    }

    public void FadeToClear()
    {
        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
        if (guiTexture.color.a <= 0.05f)
        {
            guiTexture.color = Color.clear;
            guiTexture.enabled = false;
            sceneStarting = false;
        }
    }

    public void FadeToBlack()
    {
        guiTexture.enabled = true;
        guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
    }
}
