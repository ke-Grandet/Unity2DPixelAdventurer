using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [Header("ƒ¨»œ“Ù¿÷≤•∑≈∆˜")]
    public GameObject defaultMusicPlayer;
    [Header("BOSS“Ù¿÷≤•∑≈∆˜")]
    public GameObject bossMusicPlayer;

    public static AudioController Instance;

    private AudioSource nowPlaying;
    private AudioSource _defaultAudioSource;
    private AudioSource _bossAudioSource;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(AudioController.Instance);
    }

    private void Start()
    {
        _defaultAudioSource = defaultMusicPlayer.GetComponent<AudioSource>();
        _bossAudioSource = bossMusicPlayer.GetComponent<AudioSource>();
        nowPlaying = _defaultAudioSource;
    }

    // «–ªªŒ™BOSS“Ù¿÷
    public void PlayBossMusic()
    {
        if (nowPlaying != _bossAudioSource)
        {
            nowPlaying.Stop();
            nowPlaying = _bossAudioSource;
            nowPlaying.Play();
        }
    }

    // «–ªªŒ™ƒ¨»œ“Ù¿÷
    public void PlayDefaultMusic()
    {
        if (nowPlaying != _defaultAudioSource)
        {
            nowPlaying.Stop();
            nowPlaying = _defaultAudioSource;
            nowPlaying.Play();
        }
    }

}
