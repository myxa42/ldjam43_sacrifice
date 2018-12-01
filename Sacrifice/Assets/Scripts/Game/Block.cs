using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum BlockType
{
    Empty,
    Gold,
    Crown,
    Coin,
    Dress,
    Fabric,
}

public class Block : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public const float Width = 100;
    public bool dragging;
    private float position;
    public static Building buildingUnderMouse;
    public BlockType type;

    public void SetIndex(int index)
    {
        position = index * Width;
        if (!dragging)
            GetComponent<RectTransform>().anchoredPosition = new Vector2(position, 0);
    }

    public void AddOffset(float offset)
    {
        position += offset;
        if (!dragging)
            GetComponent<RectTransform>().anchoredPosition = new Vector2(position, 0);
    }

    public float GetOffset()
    {
        return position;
    }

    public void SetOffset(float offset)
    {
        position = offset;
        if (!dragging)
            GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, 0);
    }

    public void OnDrag (PointerEventData eventData)
    {
        if (type == BlockType.Empty)
            return;
        GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;
        dragging = true;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach(var r in results)
        {
            var building = r.gameObject.GetComponent<Building>();
            if (building == null&&r.gameObject.transform.parent!=null)
            {
                building = r.gameObject.transform.parent.gameObject.GetComponent<Building>();
            }
            if (building != null)
            {
                buildingUnderMouse = building;
                return;
            }
        }
        buildingUnderMouse = null;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(position, 0);
        dragging = false;
        if (buildingUnderMouse != null&&type!=BlockType.Empty)
        {
            bool found = false;
            foreach(var r in buildingUnderMouse.requiredResources)
            {
                if (r.blockType == type)
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                buildingUnderMouse.AddResource(type);
                Destroy(gameObject);
            }
        }
        buildingUnderMouse = null;
    }
}
