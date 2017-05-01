using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DancerFieldOfView))]
public class FieldOfViewEditor : Editor
{

    public void OnSceneGUI()
    {
        DancerFieldOfView fow = (DancerFieldOfView)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.radiusRange);
    }
}

