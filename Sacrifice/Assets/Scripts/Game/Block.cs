using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BlockType
{
    Empty,
    Gold,
    Crown,
    Coin,
    Dress,
    Fabric,
}

public class Block : MonoBehaviour
{
    public const float Width = 100;

    public void SetIndex(int index)
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(index * Width, 0);
    }

    public void AddOffset(float offset)
    {
        GetComponent<RectTransform>().anchoredPosition += new Vector2(offset, 0);
    }

    public float GetOffset()
    {
        return GetComponent<RectTransform>().anchoredPosition.x;
    }

    public void SetOffset(float offset)
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, 0);
    }
}
