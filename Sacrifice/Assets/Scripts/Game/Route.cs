using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public Waypoint[] waypoints;

    public Vector2 GetLastWaypoint()
    {
        return waypoints[waypoints.Length-1].GetPosition();
    }

    public Vector2 GetWaypoint(int index)
    {
        return waypoints[index].GetPosition();
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.identity;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            var p1 = waypoints[i];
            var p2 = waypoints[i + 1];
            if(p1!=null&&p2!=null)
                Gizmos.DrawLine(p1.transform.position,p2.transform.position);
        }
    }
}
