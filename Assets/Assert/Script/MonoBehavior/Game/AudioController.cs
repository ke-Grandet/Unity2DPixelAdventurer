using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [Header("Ĭ�����ֲ�����")]
    public GameObject defaultMusicPlayer;
    [Header("BOSS���ֲ�����")]
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

    // �л�ΪBOSS����
    public void PlayBossMusic()
    {
        if (nowPlaying != _bossAudioSource)
        {
            nowPlaying.Stop();
            nowPlaying = _bossAudioSource;
            nowPlaying.Play();
        }
    }

    // �л�ΪĬ������
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
