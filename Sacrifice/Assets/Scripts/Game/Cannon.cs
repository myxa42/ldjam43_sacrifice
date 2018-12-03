using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    public GameController gameController;
    public int count;
    public Image ball1;
    public Image ball2;
    public Image ball3;
    public Image marker;
    public GameObject prefab;
    private Canvas canvas;
    public ParticleSystem particleSystem;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        marker.gameObject.SetActive(false);
    }

    void Update()
    {
        ball1.enabled = count > 0;
        ball2.enabled = count > 1;
        ball3.enabled = count > 2;

        if (Block.cannonUnderMouse == this)
        {
            GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public void AddBall()
    {
        if (count < 3)
            ++count;
    }

    public void Shoot()
    {
        if (count <= 0)
            return;

        var princes = gameController.princesInMotion;
        RoyalPerson prince = null;
        if (princes.Count > 0)
        {
            for (int i = 0; i < princes.Count; i--)
            {
                prince = princes[i];
                if (prince != null)
                    break;
            }
        }

        if (prince == null)
            return;

        --count;
        var ball = Instantiate(prefab, canvas.transform);
        ball.transform.position = marker.transform.position;
        ball.GetComponent<CannonBall>().target = prince;
        ball.GetComponent<CannonBall>().gameController = gameController;
        particleSystem.Play();
    }
}
