using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockPanel : MonoBehaviour
{
    public Blocks BlockList;
    private static System.Random random = new System.Random();
    int init = 2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (--init != 0)
            return;

        var image = GetComponent<Image>();
        Debug.Log($"{image.rectTransform.rect.width}");
        for (int i = 0; i < 11; i++)
        {
            int index = random.Next(0, BlockList.Prefabs.Length);
            var go = Instantiate(BlockList.Prefabs[index]);
            go.transform.SetParent(transform);
            go.GetComponent<Block>().SetIndex(i);
        }

    }
}
