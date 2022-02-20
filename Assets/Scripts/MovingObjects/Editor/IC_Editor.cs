using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(InvisibleCar))]
[CanEditMultipleObjects]
public class IC_Editor : Editor
{
    bool designPart, debugPart, otherpart, moveConfig, crimeConfig, unicConfig = false;

    //Movement Fields
    SerializedProperty currentwaypoint;
    SerializedProperty maxSpeed;
    SerializedProperty moveAcc;
    SerializedProperty stopAcc;

    //Main crime Fields
    SerializedProperty allowMinus;
    SerializedProperty mainPunishValue;
    SerializedProperty mainTaptime;

    //Debug Fields
    SerializedProperty crimeState;
    SerializedProperty speed;

    //Other Fields
    SerializedProperty fadeSprites;
    SerializedProperty csc_TopSprite;
    SerializedProperty csc_BotSprite;
    SerializedProperty csc_SideSprite;

    //Unic Fields
    SerializedProperty laserTime;

    GUIStyle headerStyle = new GUIStyle();
    

    private void OnEnable()
    {
        headerStyle.fontStyle = FontStyle.BoldAndItalic;
        DefineSerializeProperties();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawCustomInspector();
        serializedObject.ApplyModifiedProperties();
    }

    void DefineSerializeProperties()
    {
        currentwaypoint = serializedObject.FindProperty("currentWaypoint");
        maxSpeed = serializedObject.FindProperty("maxSpeed");
        moveAcc = serializedObject.FindProperty("moveAccelerate");
        stopAcc = serializedObject.FindProperty("stopAccelerate");

        allowMinus = serializedObject.FindProperty("noTapPunishValue");
        mainPunishValue = serializedObject.FindProperty("mainPunishValue");
        mainTaptime = serializedObject.FindProperty("main_TapAllowedTime");

        crimeState = serializedObject.FindProperty("isDoingSomeCrime");
        speed = serializedObject.FindProperty("currentSpeed");

        fadeSprites = serializedObject.FindProperty("carSprites");
        csc_SideSprite = serializedObject.FindProperty("leftAndRightSprite");
        csc_TopSprite = serializedObject.FindProperty("topSprite");
        csc_BotSprite = serializedObject.FindProperty("botSprite");

        laserTime = serializedObject.FindProperty("LaserTime");
    }

    void DrawCustomInspector()
    {
        designPart = EditorGUILayout.Foldout(designPart, "■ Design Fields", true, headerStyle);
        if (designPart)
        {
            EditorGUI.indentLevel++;
            moveConfig = EditorGUILayout.Foldout(moveConfig, "Movement Config", true, headerStyle);
            if (moveConfig)
            {
                EditorGUILayout.PropertyField(currentwaypoint);
                EditorGUILayout.PropertyField(maxSpeed);
                EditorGUILayout.PropertyField(moveAcc);
                EditorGUILayout.PropertyField(stopAcc);
            }

            crimeConfig = EditorGUILayout.Foldout(crimeConfig, "Crime Config", true, headerStyle);
            if (crimeConfig)
            {
                EditorGUILayout.PropertyField(allowMinus);
                EditorGUILayout.PropertyField(mainPunishValue);
                EditorGUILayout.PropertyField(mainTaptime);
            }

            unicConfig = EditorGUILayout.Foldout(unicConfig, "Unic Config", true, headerStyle);
            if (unicConfig)
            {
                EditorGUILayout.PropertyField(laserTime);
            }
        }

        EditorGUI.indentLevel = 0;
        debugPart = EditorGUILayout.Foldout(debugPart, "■ Debug Fields", true, headerStyle);
        if (debugPart)
        {
            EditorGUILayout.PropertyField(crimeState);
            EditorGUILayout.PropertyField(speed);
        }

        otherpart = EditorGUILayout.Foldout(otherpart, "■ Other Fields", true, headerStyle);
        if (otherpart)
        {
            EditorGUILayout.PropertyField(fadeSprites);
            EditorGUILayout.PropertyField(csc_SideSprite);
            EditorGUILayout.PropertyField(csc_TopSprite);
            EditorGUILayout.PropertyField(csc_BotSprite);
        }
    }
}
