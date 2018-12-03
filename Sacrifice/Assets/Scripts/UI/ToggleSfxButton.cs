using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ToggleSfxButton : MonoBehaviour
{
    public Image cross;
    public AudioSource sound;

    public void Toggle()
    {
        sound.Play();
        SoundController.SetSoundEnabled(!SoundController.IsSoundEnabled());
    }

    void Update()
    {
        cross.enabled = !SoundController.IsSoundEnabled();
    }
}
