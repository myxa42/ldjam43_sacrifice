using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Queue<BlockType> pendingBlocks = new Queue<BlockType>();
    public float coinSpawnFrequency = 1;
    private float timeUntilNextCoin;
    public Canvas canvas;
    public Route route;
    public Dragon dragon;
    public GameObject princessPrefab;
    public GameObject princePrefab;
    public Text sacrificedText;
    [System.NonSerialized] public int sacrificedCount;
    public List<RoyalPerson> princesInMotion = new List<RoyalPerson>();
    public GameObject deadPrincePrefab;
    public GameObject pauseMenu;

    void Start()
    {
        SoundController.defeat = false;
        pauseMenu.SetActive(false);
    }

    public void SpawnPrincess()
    {
        SpawnRoyalPerson(princessPrefab);
    }

    public void SpawnPrince()
    {
        var person = SpawnRoyalPerson(princePrefab);
        princesInMotion.Add(person);
    }

    private RoyalPerson SpawnRoyalPerson(GameObject prefab)
    {
        var person = Instantiate(prefab, canvas.transform).GetComponent<RoyalPerson>();
        person.gameController = this;
        person.route = route;
        return person;
    }

    public void Pause()
    {
        if (!pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Unpause()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    private void Update()
    {
        sacrificedText.text = $"Princesses sacrificed: {sacrificedCount}";

        timeUntilNextCoin -= Time.deltaTime;
        if (timeUntilNextCoin <= 0)
        {
            timeUntilNextCoin = coinSpawnFrequency;
            pendingBlocks.Enqueue(BlockType.Coin);
        }
    }
}
