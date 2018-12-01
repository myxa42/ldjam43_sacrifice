using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [System.Serializable]
    public struct Resource
    {
        public BlockType blockType;
        public int count;
    }

    public Image activeImage;
    public Image inactiveImage;
    public Resource[] requiredResources;
    private Dictionary<BlockType, int> availableResources = new Dictionary<BlockType, int>();
    public GameController gameController;
    public BlockType product=BlockType.Empty;

    // Start is called before the first frame update
    void Start()
    {
        activeImage.enabled = false;
        inactiveImage.enabled = true;
    }

    public void AddResource(BlockType type,int count=1)
    {
        availableResources[type] += count;
    }

    // Update is called once per frame
    void Update()
    {
        bool haveEnoughResources = true;
        foreach(var res in requiredResources)
        {
            int count;
            if(!availableResources.TryGetValue(res.blockType,out count)||count < res.count)
            {
                haveEnoughResources = false;
                break;
            }
        }

        if (!haveEnoughResources)
        {
            return;
        }

        foreach (var res in requiredResources)
        {
            availableResources[res.blockType] -= res.count;
        }

        if (product != BlockType.Empty)
        {
            gameController.pendingBlocks.Enqueue(product);
        }
    }
}
