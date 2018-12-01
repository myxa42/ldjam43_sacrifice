using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockPanel : MonoBehaviour
{
    public Blocks BlockList;
    private List<Block> blocks = new List<Block>();
    public float Speed=1;
    public GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        var image = GetComponent<Image>();
        for (float x = 0; x <= image.rectTransform.rect.width + Block.Width / 2; x += Block.Width)
        {
            var go = Instantiate(BlockList.empty);
            go.transform.SetParent(transform);
            var block = go.GetComponent<Block>();
            block.SetIndex(i++);
            blocks.Add(block);
        }
    }

    private void FixedUpdate()
    {
        float x = 0;
        for(int i=0;i<blocks.Count;i++)
        {
            var block = blocks[i];
            block.AddOffset(-Speed);
            float blockX = block.GetOffset();
            if (blockX < -Block.Width / 2)
            {
                Destroy(block.gameObject);
                blocks.RemoveAt(i);
                i--;
            }
            else
            {
                x = Mathf.Max(x, blockX);
            }
        }
        x += Block.Width;
        var image = GetComponent<Image>();
        while (x<= image.rectTransform.rect.width + Block.Width / 2)
        {
            BlockType type = BlockType.Empty;
            if (gameController.pendingBlocks.Count>0)
            {
                type = gameController.pendingBlocks.Dequeue();
            }

            var go = Instantiate(BlockList.GetPrefab(type));
            go.transform.SetParent(transform);
            var block = go.GetComponent<Block>();
            block.SetOffset(x);
            blocks.Add(block);
            x += Block.Width;
        }
    }
}
