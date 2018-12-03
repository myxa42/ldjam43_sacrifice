using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameController))]
public class PrincessSpawnTool : Editor
{
    public override void OnInspectorGUI()
    {
        if (Application.isPlaying)
        {
            if (GUILayout.Button("Spawn princess"))
                ((GameController)target).SpawnPrincess();
            if (GUILayout.Button("Spawn prince"))
                ((GameController)target).SpawnPrince();
            if (GUILayout.Button("Add cannon shot"))
                FindObjectOfType<Cannon>().AddBall();
        }

        base.OnInspectorGUI();
    }
}
