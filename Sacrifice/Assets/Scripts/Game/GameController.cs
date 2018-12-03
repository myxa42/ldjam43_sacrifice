using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
