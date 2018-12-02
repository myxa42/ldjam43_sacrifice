using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks")]
public class Blocks : ScriptableObject
{
    public GameObject empty;
    public GameObject gold;
    public GameObject crown;
    public GameObject coin;
    public GameObject dress;
    public GameObject fabric;
    public GameObject bomb;
    public GameObject cake;
    public GameObject wheat;
    public GameObject bread;
    public GameObject tools;


    public GameObject GetPrefab(BlockType type)
    {
        switch (type)
        {
            case BlockType.Empty:return empty;
            case BlockType.Gold: return gold;
            case BlockType.Coin: return coin;
            case BlockType.Crown: return crown;
            case BlockType.Fabric: return fabric;
            case BlockType.Dress: return dress;
            case BlockType.Bomb: return bomb;
            case BlockType.Cake: return cake;
            case BlockType.Wheat: return wheat;
            case BlockType.Bread: return bread;
            case BlockType.Tools: return tools;
        }
        return null;
    }
}
