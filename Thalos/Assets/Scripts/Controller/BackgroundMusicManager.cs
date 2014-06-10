using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Backend;

public class BackgroundMusicManager : MonoBehaviour {

    public int trackCount;
    private bool isMuted = false;

    public AudioClip deadMusicTrack;
    public AudioClip mainMenuMusicTrack;
    public AudioClip epicBattleTracks;
    public AudioClip[] normalMusicTracks;
    public AudioClip[] battleMusicTracks;

    private List<AudioSource> channelList;

    private float maxVolume = 100f;
    private float volumeSteps = 0.01f;
    private int mainChannel = Constants.NORMALMUSIC_CHANNEL;
    
    void Awake()
    {
        
        //SetStopAllMusic();
        //SetMusicVolume(0);
        //SetMuteMusic(true);
    }

    void Start()
    {
        Gamestatemanager.PlayerIsDeadHandler += FadeDeteatedMusicIn;
        Gamestatemanager.OpenPlayScreenHandler += FadeNormalMusicIn;
        Gamestatemanager.OpenPlayScreenHandler += UnMuteAllTracks;
        Gamestatemanager.OpenMainMenuHandler += FadeMainMenuSoundTrack;
        Gamestatemanager.CloseMainMenuScreenHandler += MuteAllMusic;

        InitChannelList();
        CheckGameState();

    }

    private void CheckGameState()
    {
        Gamestatemanager.Gamestate state = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Gamestatemanager>().actualState;

        switch (state)
        {
            case Gamestatemanager.Gamestate.Mainmenu:
                mainChannel = Constants.MAINMENUMUSIC_CHANNEL;
                break;

            case Gamestatemanager.Gamestate.Play:
                mainChannel = Constants.NORMALMUSIC_CHANNEL;
                break;
        }
    }

    private void InitChannelList()
    {
        Debug.Log("InitChannelList");
        channelList = new List<AudioSource>();

        for (int i = 0; i < trackCount; i++)
        {
            AudioSource src = this.gameObject.AddComponent<AudioSource>(); 


            switch (i)
            {
                case Constants.MAINMENUMUSIC_CHANNEL:
                    src.clip = mainMenuMusicTrack;
                    break;

                case Constants.NORMALMUSIC_CHANNEL:
                    if (normalMusicTracks != null)
                    {
                        src.clip = normalMusicTracks[0];
                    }
                    break;

                case Constants.BATTLEMUSIC_CHANNEL:
                    if (battleMusicTracks != null)
                    {
                        src.clip = battleMusicTracks[0];
                    }
                    break;

                case Constants.DEADMUSIC_CHANNEL:
                    src.clip = deadMusicTrack;
                    break;

            }


            channelList.Add(src);
        }
    }

    private void SetGlobalMusicVolume(int volume)
    {
        foreach (AudioSource src in channelList)
        {
            src.volume = volume;
        }
    }

    private void StopAllMusic()
    {
        foreach (AudioSource src in channelList)
        {
            src.Stop();
        }
    }
    
    private void FadeMainMenuSoundTrack()
    {
        mainChannel = Constants.MAINMENUMUSIC_CHANNEL;
    }

    private void FadeDeteatedMusicIn()
    {
        mainChannel = Constants.DEADMUSIC_CHANNEL;
    }

    private void FadeBattleMusicIn()
    {
        mainChannel = Constants.BATTLEMUSIC_CHANNEL;
    }

    private void FadeNormalMusicIn()
    {
        mainChannel = Constants.NORMALMUSIC_CHANNEL;
    }

    private void MuteAllMusic()
    {
        this.isMuted = true;
    }

    private void UnMuteAllTracks()
    {
        this.isMuted = false;
    }
    void Update()
    {
        if (!isMuted)
        {
            if (!channelList[mainChannel].isPlaying)
            {
                channelList[mainChannel].Play();
            }

            if (channelList[mainChannel].volume < maxVolume)
            {
                channelList[mainChannel].volume += volumeSteps;
            }

            for (int i = 0; i < channelList.Count; i++)
            {
                if (i != mainChannel)
                {
                    if (channelList[i].volume > 0.01f && channelList[i].isPlaying)
                    {
                        channelList[i].volume -= volumeSteps;
                    }
                    else
                    {
                        channelList[i].Stop();
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < channelList.Count; i++)
            {
                    if (channelList[i].volume > 0.01f && channelList[i].isPlaying)
                    {
                        channelList[i].volume -= volumeSteps;
                    }
                    else
                    {
                        channelList[i].Stop();
                    }
            }
        }

    }
}
