using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Vector2 GetPosition()
    {
        return GetComponent<RectTransform>().anchoredPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(10, 10, 10));
    }
}
