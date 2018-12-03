using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPrince : MonoBehaviour
{
    public float timeUntilDisappear = 3;

    // Update is called once per frame
    void Update()
    {
        timeUntilDisappear -= Time.deltaTime;
        if (timeUntilDisappear <= 0)
        {
            Destroy(gameObject);
        }
    }
}
