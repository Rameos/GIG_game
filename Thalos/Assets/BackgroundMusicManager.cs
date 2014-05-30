using UnityEngine;
using System.Collections;

public class BackgroundMusicManager : MonoBehaviour {

    private bool isfadeInActive=false;
    private bool isfadeOutActive = false;
    private float maxVolume = 100f;
    private float volumeSteps = 0.1f;

    public void playMainMenuSoundTrack()
    {

    }

    void Update()
    {
        if (isfadeInActive)
        {

            audio.enabled = true;
            if (audio.volume <= 100)
            {
                audio.volume += 0.1f;
            }
            else
            {
                isfadeInActive = true; 
            }
        }
        else if (isfadeOutActive)
        {
            if (audio.volume <= 0)
            {
                audio.enabled = false;
                isfadeOutActive = false;
            }
            else
            {
                audio.volume -= 0.1f;
            }
        }
    }
}
