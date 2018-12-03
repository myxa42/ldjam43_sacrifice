using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    void Awake()
    {
        SoundController.defeat = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
