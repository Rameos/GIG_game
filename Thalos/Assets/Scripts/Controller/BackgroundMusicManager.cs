using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Backend;

public class BackgroundMusicManager : MonoBehaviour {


    public AudioSource deadMusicSlot;
    public AudioSource normalMusicSlot;
    public AudioSource battleMusicSlot;
    public AudioSource mainMenuMusicSlot;

    private List<AudioSource> channelList;

    public AudioClip[] normalMusicTracks;
    public AudioClip[] battleMusicTracks;

    private float maxVolume = 100f;
    private float volumeSteps = 0.1f;


    private bool isDeadMusicPlaying;
    private bool isMainMenuPlaying;
    private bool isBattleMusicPlaying;
    private bool isNormalMusicPlaying; 


    void Awake()
    {
        InitChannelList();
        //SetStopAllMusic();
        //SetMusicVolume(0);
        //SetMuteMusic(true);
    }

    void Start()
    {
        Gamestatemanager.PlayerIsDeadHandler += FadeDeteatedMusicIn;

    }

    private void InitChannelList()
    {
        channelList = new List<AudioSource>();
        channelList.Add(deadMusicSlot);
        channelList.Add(normalMusicSlot);
        channelList.Add(battleMusicSlot);
        channelList.Add(mainMenuMusicSlot);
        
    }

    private void SetMusicVolume(int volume)
    {
        foreach (AudioSource src in channelList)
        {
            src.volume = volume;
        }
    }

    private void SetMuteMusic(bool isMuted)
    {
        foreach (AudioSource src in channelList)
        {
            src.mute = isMuted;
        }
    }

    private void SetStopAllMusic()
    {
        foreach (AudioSource src in channelList)
        {
            src.Stop();
        }
    }
    
    private void FadeMainMenuSoundTrack()
    {

    }

    private void FadeDeteatedMusicIn()
    {
        Debug.Log("StartDeadMusic");

        if (!deadMusicSlot.isPlaying)
        {
            deadMusicSlot.Play();
        }
    }

    private void FadeBattleMusicIn()
    {

    }

    private void FadeNormalMusicIn()
    {
        
    }

    void Update()
    {

    }
}
