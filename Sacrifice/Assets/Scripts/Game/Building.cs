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
    public BlockType[] products;
    public float workTime = 1;
    private float timeLeft;
    private bool building;
    public bool isCastle;
    public int princessSpawnProbability;
    public int princeSpawnProbability;
    public Text CrownCount;
    public Text CakeCount;
    public Text DressCount;

    // Start is called before the first frame update
    void Start()
    {
        activeImage.enabled = false;
        inactiveImage.enabled = true;
    }

    public void AddResource(BlockType type,int count=1)
    {
        int n=0;
        availableResources.TryGetValue(type, out n);
        availableResources[type] = n+count;
    }

    // Update is called once per frame
    void Update()
    {
        if (DressCount != null)
        {
            int n = 0;
            availableResources.TryGetValue(BlockType.Dress, out n);
            DressCount.text = n.ToString();
        }

        if (CakeCount != null)
        {
            int n = 0;
            availableResources.TryGetValue(BlockType.Cake, out n);
            CakeCount.text = n.ToString();
        }

        if (CrownCount != null)
        {
            int n = 0;
            availableResources.TryGetValue(BlockType.Crown, out n);
            CrownCount.text = n.ToString();
        }

        if (Block.buildingUnderMouse == this)
        {
            activeImage.color = new Color(0, 1, 0, 1);
            inactiveImage.color = new Color(0, 1, 0, 1);
        }
        else
        {
            activeImage.color = new Color(1, 1, 1, 1);
            inactiveImage.color = new Color(1, 1, 1, 1);
        }

        timeLeft -= Time.deltaTime;
        if (timeLeft > 0)
        {
            activeImage.enabled = true;
            inactiveImage.enabled = false;
            return;
        }
        else
        {
            if (building)
            {
                building = false;
                if (products.Length > 0)
                {
                    int index = RandomGenerator.NextInt(0, products.Length);
                    gameController.pendingBlocks.Enqueue(products[index]);
                }
                if (isCastle)
                {
                    int total = princeSpawnProbability + princessSpawnProbability;
                    int n = RandomGenerator.NextInt(0, total);
                    if (n < princeSpawnProbability)
                        gameController.SpawnPrince();
                    else
                        gameController.SpawnPrincess();
                }
            }
            activeImage.enabled = false;
            inactiveImage.enabled = true;
            timeLeft = 0;
        }

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

        building = true;
        timeLeft = workTime;
        activeImage.enabled = true;
        inactiveImage.enabled = false;
    }
}
