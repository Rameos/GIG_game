using UnityEngine;
using System.Collections;

public class UISoundManager : MonoBehaviour 
{

    public void playClickSound()
    {
        audio.clip = Resources.Load("Sounds/clickProject", typeof(AudioClip)) as AudioClip;
        audio.Play();
    }
}
