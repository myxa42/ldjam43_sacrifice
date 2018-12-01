using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public const float Width = 100;

    public void SetIndex(int index)
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(index * Width, 0);
    }
}
