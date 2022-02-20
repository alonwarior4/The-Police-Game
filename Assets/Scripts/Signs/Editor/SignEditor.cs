using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;




[CustomEditor(typeof(Sign))]
[CanEditMultipleObjects]
public class SignEditor : Editor
{
    bool designPart , otherPart = false;

    //Design Part
    SerializedProperty signType;
    SerializedProperty crimeType;
    SerializedProperty isMainCrime;
    SerializedProperty signCurve;
    SerializedProperty tappedCars;
    SerializedProperty completeSignSp;

    //Other parts
    SerializedProperty laserMat;
    SerializedProperty startPos;
    SerializedProperty mainCollObj;
    SerializedProperty signSpPlace;

    //header style
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

    void DefineSerializedProperties()
    {
        signType = serializedObject.FindProperty("Type");
        crimeType = serializedObject.FindProperty("signCrimeType");
        isMainCrime = serializedObject.FindProperty("isMainCrimeAvailable");
        signCurve = serializedObject.FindProperty("mainDefecaultyCurve");
        tappedCars = serializedObject.FindProperty("carsTappedInSignCrime");
        completeSignSp = serializedObject.FindProperty("completeSP");

        laserMat = serializedObject.FindProperty("laserMaterial");
        startPos = serializedObject.FindProperty("lineRendererStartPos");
        mainCollObj = serializedObject.FindProperty("mainColliderObj");
        signSpPlace = serializedObject.FindProperty("signSpritePlace");
    }

    private void DrawCustomInspector()
    {
        designPart = EditorGUILayout.Foldout(designPart, "■ Design Fields", true, headerStyle);
        if (designPart)
        {
            EditorGUILayout.PropertyField(signType);
            EditorGUILayout.PropertyField(crimeType);
            EditorGUILayout.PropertyField(completeSignSp);
            EditorGUILayout.PropertyField(isMainCrime);
            EditorGUILayout.PropertyField(signCurve);
            EditorGUILayout.PropertyField(tappedCars);
        }

        otherPart = EditorGUILayout.Foldout(otherPart, "■ Other Fields", true, headerStyle);
        if (otherPart)
        {
            EditorGUILayout.PropertyField(laserMat);
            EditorGUILayout.PropertyField(startPos);
            EditorGUILayout.PropertyField(mainCollObj);
            EditorGUILayout.PropertyField(signSpPlace);
        }
    }
}



[CustomEditor(typeof(_Camera))]
[CanEditMultipleObjects]
public class CameraEditor : Editor
{
    bool designPart, otherPart = false;

    //Design Parts
    SerializedProperty cameraCurve;
    SerializedProperty completeSP;

    //Other Parts
    SerializedProperty camSpPlace;
    SerializedProperty camColl;

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

    void DefineSerializedProperties()
    {
        cameraCurve = serializedObject.FindProperty("sideDefecaultyCurve");
        completeSP = serializedObject.FindProperty("completeSp");

        camSpPlace = serializedObject.FindProperty("cameraSpritePlace");
        camColl = serializedObject.FindProperty("cameraCollider");
    }

    void DrawCustomInspector()
    {
        designPart = EditorGUILayout.Foldout(designPart, "■ Design Fields", true, headerStyle);
        if (designPart)
        {
            EditorGUILayout.PropertyField(completeSP);
            EditorGUILayout.PropertyField(cameraCurve);
        }

        otherPart = EditorGUILayout.Foldout(otherPart, "■ Other Fields", true, headerStyle);
        if (otherPart)
        {
            EditorGUILayout.PropertyField(camSpPlace);
            EditorGUILayout.PropertyField(camColl);
        }
    }
}