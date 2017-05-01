using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SingerFieldOfView))]
public class FieldOfSingEditor : Editor
{

    public void OnSceneGUI()
    {
        SingerFieldOfView fow = (SingerFieldOfView)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.radiusRange);
    }
}