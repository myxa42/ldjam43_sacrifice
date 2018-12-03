using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMusicButton : MonoBehaviour
{
    public Image cross;
    public AudioSource sound;

    public void Toggle()
    {
        sound.Play();
        SoundController.SetMusicEnabled(!SoundController.IsMusicEnabled());
    }

    void Update()
    {
        cross.enabled = !SoundController.IsMusicEnabled();
    }
}
