using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    private static bool soundEnabled;
    private static bool musicEnabled;
    private static SoundController instance;
    public AudioSource normalMusicSource;
    public AudioSource defeatMusicSource;
    public AudioMixer mixer;
    public static bool defeat;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) != 0;
            musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) != 0;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public static bool IsSoundEnabled()
    {
        return soundEnabled;
    }

    public static bool IsMusicEnabled()
    {
        return musicEnabled;
    }

    public static void SetSoundEnabled(bool flag)
    {
        if (soundEnabled != flag)
        {
            soundEnabled = flag;
            PlayerPrefs.SetInt("SoundEnabled", flag ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static void SetMusicEnabled(bool flag)
    {
        if (musicEnabled != flag)
        {
            musicEnabled = flag;
            PlayerPrefs.SetInt("MusicEnabled", flag ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        bool playNormal = musicEnabled && !defeat;
        bool playDefeat = musicEnabled && defeat;

        if (playNormal && !normalMusicSource.isPlaying)
            normalMusicSource.Play();
        else if (!playNormal && normalMusicSource.isPlaying)
            normalMusicSource.Stop();

        if (playDefeat && !defeatMusicSource.isPlaying)
            defeatMusicSource.Play();
        else if (!playDefeat && defeatMusicSource.isPlaying)
            defeatMusicSource.Stop();

        float volume;
        if (mixer.GetFloat("SfxVolume", out volume))
        {
            if (soundEnabled && volume < -1)
                mixer.SetFloat("SfxVolume", 0);
            else if (!soundEnabled && volume > -1)
                mixer.SetFloat("SfxVolume", -80);
        }
    }
}
