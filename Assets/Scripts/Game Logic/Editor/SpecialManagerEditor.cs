using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpecialCarManager))]
public class SpecialManagerEditor : Editor
{
    bool designPart, debugPart, otherPart = false;

    //design fields
    SerializedProperty curve;
    SerializedProperty pathes;

    //debug fields
    SerializedProperty sp_InScene;
    SerializedProperty sp_InQueue;

    //other fields
    SerializedProperty sp_Cars;
    //SerializedProperty crimeWaypoints;

    GUIStyle headerStyle = new GUIStyle();


    private void OnEnable()
    {
        headerStyle.fontStyle = FontStyle.BoldAndItalic;
        DefineSerializedProperties();
    }    

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawCustomInspector();
        serializedObject.ApplyModifiedProperties();
    }    

    private void DefineSerializedProperties()
    {
        curve = serializedObject.FindProperty("LogicCurve");
        pathes = serializedObject.FindProperty("specialPathes");

        sp_InScene = serializedObject.FindProperty("isSpecialCarInScene");
        sp_InQueue = serializedObject.FindProperty("isSpecialCarInQueue");

        sp_Cars = serializedObject.FindProperty("specialCars");
        //crimeWaypoints = serializedObject.FindProperty("mainCrimeWaypoints");
    }

    private void DrawCustomInspector()
    {
        designPart = EditorGUILayout.Foldout(designPart, "■ Design Fields", true, headerStyle);
        if (designPart)
        {
            EditorGUILayout.PropertyField(curve);
            EditorGUILayout.PropertyField(pathes);
        }

        debugPart = EditorGUILayout.Foldout(debugPart, "■ Debug Fields", true, headerStyle);
        if (debugPart)
        {
            EditorGUILayout.PropertyField(sp_InScene);
            EditorGUILayout.PropertyField(sp_InQueue);
        }

        otherPart = EditorGUILayout.Foldout(otherPart, "■ Other Fields", true, headerStyle);
        if (otherPart)
        {
            EditorGUILayout.PropertyField(sp_Cars);
            //EditorGUILayout.PropertyField(crimeWaypoints);
        }
    }

}
