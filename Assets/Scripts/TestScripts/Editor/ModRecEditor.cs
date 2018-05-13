using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ModRecScript))]
public class ModRecEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ModRecScript modRec = (ModRecScript) target;
        if (GUILayout.Button("Add Character"))
        {
            modRec.TestHWRRec();
        }
    }
}