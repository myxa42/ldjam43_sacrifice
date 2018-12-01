using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Queue<BlockType> pendingBlocks = new Queue<BlockType>();

    private void Update()
    {
        while (pendingBlocks.Count < 10)
        {
            pendingBlocks.Enqueue((BlockType)new System.Random().Next(0, 5));
        }
    }
}
