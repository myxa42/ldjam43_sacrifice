using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    private static System.Random random = new System.Random();

    public static int NextInt (int first, int end)
    {
        return random.Next(first, end);
    }

}
