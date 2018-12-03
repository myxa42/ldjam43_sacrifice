using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoyalPerson : MonoBehaviour
{
    public bool isEdible;
    public Route route;
    private float progress;
    public Image normal;
    public Image surprised;
    public Image unhappy;
    public float speed = 1;
    [System.NonSerialized] public GameController gameController;
    public bool arrived;
    public AudioSource uhohSound;
    private bool seenDragon;

    private void Start()
    {
        normal.enabled = true;
        surprised.enabled = false;
        unhappy.enabled = false;
    }

    private void OnDestroy()
    {
        gameController.princesInMotion.Remove(this);
    }

    private void SetPosition(Vector2 pos)
    {
        GetComponent<RectTransform>().anchoredPosition = pos;
    }

    private void Update()
    {
        progress += Time.deltaTime*speed;
        int waypoint = (int)progress;
        if (waypoint >= route.waypoints.Length - 1)
        {
            SetPosition(route.GetLastWaypoint());
            normal.enabled = false;
            surprised.enabled = false;
            unhappy.enabled = true;
            if (!arrived)
            {
                arrived = true;
                gameController.dragon.Eat(this);
            }
        }
        else
        {
            Vector2 pos1 = route.GetWaypoint(waypoint);
            Vector2 pos2 = route.GetWaypoint(waypoint+1);
            SetPosition(pos1 + (pos2 - pos1) * (progress - waypoint));
            normal.enabled = waypoint<route.waypoints.Length-3;
            surprised.enabled = waypoint == route.waypoints.Length - 3;
            unhappy.enabled = waypoint >= route.waypoints.Length - 2;
        }

        if (unhappy.enabled)
        {
            if (!seenDragon)
            {
                seenDragon = true;
                uhohSound.Play();
            }
        }
    }
}
