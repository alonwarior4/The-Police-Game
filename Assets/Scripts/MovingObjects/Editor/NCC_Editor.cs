using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
 


[CustomEditor(typeof(NormalCrimeCar))]
[CanEditMultipleObjects]
public class NCC_Editor : Editor
{
    bool designPart , debugPart , otherPart , soundPart ,  moveConfig, mainConfig, sideConfig = false;

    //movement Fields
    SerializedProperty currentwaypoint;
    SerializedProperty maxSpeed;
    SerializedProperty moveAcc;
    SerializedProperty stopAcc;

    //Main crime Fields
    SerializedProperty allowMinus;
    SerializedProperty mainPunishValue;
    SerializedProperty mainTaptime;
    SerializedProperty mainChance;

    //Side Crime Fields
    SerializedProperty sideChance;
    SerializedProperty sidePunishValue;
    SerializedProperty sideTapTime;

    //Debug Fields
    SerializedProperty nextWaypoint;
    SerializedProperty crimeState;
    SerializedProperty speed;

    //Other Fields
    SerializedProperty randomSp;
    SerializedProperty randomSpPlace;
    SerializedProperty popUpAnim;
    SerializedProperty fadeSprites;
    SerializedProperty csc_TopSprite;
    SerializedProperty csc_BotSprite;
    SerializedProperty csc_SideSprite;

    //sound fields
    SerializedProperty popSound;
    SerializedProperty popTapSound;

    GUIStyle headerStyle = new GUIStyle();
    GUIStyle fieldHeaderStyle = new GUIStyle();

    private void OnEnable()
    {
        headerStyle.fontStyle = FontStyle.BoldAndItalic;
        fieldHeaderStyle.fontStyle = FontStyle.Bold;
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
        mainChance = serializedObject.FindProperty("mainCrimeChance");

        sideChance = serializedObject.FindProperty("sideCrimeChance");
        sidePunishValue = serializedObject.FindProperty("sidePunishValue");
        sideTapTime = serializedObject.FindProperty("side_TapAllowedTime");

        crimeState = serializedObject.FindProperty("isDoingSomeCrime");
        speed = serializedObject.FindProperty("currentSpeed");
        nextWaypoint = serializedObject.FindProperty("nextWaypoint");

        randomSp = serializedObject.FindProperty("randomSprites");
        randomSpPlace = serializedObject.FindProperty("randomSpritePlace");
        popUpAnim = serializedObject.FindProperty("popUpAnim");
        fadeSprites = serializedObject.FindProperty("carSprites");
        csc_SideSprite = serializedObject.FindProperty("leftAndRightSprite");
        csc_TopSprite = serializedObject.FindProperty("topSprite");
        csc_BotSprite = serializedObject.FindProperty("botSprite");

        popSound = serializedObject.FindProperty("popUpSound");
        popTapSound = serializedObject.FindProperty("popUpTapSound");
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

            mainConfig = EditorGUILayout.Foldout(mainConfig, "Main Crime Config", true, headerStyle);

            if (mainConfig)
            {
                EditorGUILayout.PropertyField(mainChance);
                EditorGUILayout.PropertyField(mainTaptime);
                EditorGUILayout.PropertyField(mainPunishValue);
                EditorGUILayout.PropertyField(allowMinus);
            }

            sideConfig = EditorGUILayout.Foldout(sideConfig, "Side Crime Config", true, headerStyle);

            if (sideConfig)
            {
                EditorGUILayout.PropertyField(sideChance);
                EditorGUILayout.PropertyField(sideTapTime);
                EditorGUILayout.PropertyField(sidePunishValue);
            }
        }

        EditorGUI.indentLevel = 0;
        debugPart = EditorGUILayout.Foldout(debugPart, "■ Debug Fields", true, headerStyle);
        if (debugPart)
        {
            EditorGUILayout.PropertyField(crimeState);
            EditorGUILayout.PropertyField(speed);
            EditorGUILayout.LabelField("Next Vehicle Waypoint" , fieldHeaderStyle);
            EditorGUILayout.PropertyField(nextWaypoint);
        }

        soundPart = EditorGUILayout.Foldout(soundPart, "■ Sound Fields", true, headerStyle);
        if (soundPart)
        {
            EditorGUILayout.PropertyField(popSound);
            EditorGUILayout.PropertyField(popTapSound);
        }

        otherPart = EditorGUILayout.Foldout(otherPart, "■ Other Fields", true, headerStyle);
        if (otherPart)
        {
            EditorGUILayout.PropertyField(randomSp);
            EditorGUILayout.PropertyField(randomSpPlace);
            EditorGUILayout.PropertyField(popUpAnim);
            EditorGUILayout.PropertyField(fadeSprites);
            EditorGUILayout.PropertyField(csc_SideSprite);
            EditorGUILayout.PropertyField(csc_TopSprite);
            EditorGUILayout.PropertyField(csc_BotSprite);
        }
    }
}


