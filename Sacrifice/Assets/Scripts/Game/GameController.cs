using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Queue<BlockType> pendingBlocks = new Queue<BlockType>();
    public float coinSpawnFrequency = 1;
    private float timeUntilNextCoin;

    private void Update()
    {
        timeUntilNextCoin -= Time.deltaTime;
        if (timeUntilNextCoin <= 0)
        {
            timeUntilNextCoin = coinSpawnFrequency;
            pendingBlocks.Enqueue(BlockType.Coin);
        }
    }
}
