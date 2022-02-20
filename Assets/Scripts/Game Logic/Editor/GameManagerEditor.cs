using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    bool designPart, otherPart = false;

    //design fields
    SerializedProperty fadeTime;
    SerializedProperty threshold;
    SerializedProperty rayLength;
    SerializedProperty maxTappedCar;
    SerializedProperty maxAllowedCar;
    SerializedProperty dragTimeScale;

    //other fileds
    SerializedProperty tappedSlider;
    //SerializedProperty BotUIPanel;
    SerializedProperty garage;
    SerializedProperty whiteText;
    SerializedProperty blueText;
    SerializedProperty uiManager;

    //light Fields
    SerializedProperty Lights;
    SerializedProperty OffLightPolice;


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
        fadeTime = serializedObject.FindProperty("vehicleFadeTime");
        threshold = serializedObject.FindProperty("changeWaypointThreshold");
        rayLength = serializedObject.FindProperty("rayLength");
        maxTappedCar = serializedObject.FindProperty("maxTapCars");
        maxAllowedCar = serializedObject.FindProperty("maxAllowedCars");
        dragTimeScale = serializedObject.FindProperty("timeScaleWhenDragSign");

        tappedSlider = serializedObject.FindProperty("tappedSlider");
        //BotUIPanel = serializedObject.FindProperty("botUiPanel");
        garage = serializedObject.FindProperty("tappedCarGarage");
        whiteText = serializedObject.FindProperty("whiteMoneyTextPlace");
        blueText = serializedObject.FindProperty("blueMoneyTextPlace");
        uiManager = serializedObject.FindProperty("winLoseUi");

        Lights = serializedObject.FindProperty("Lights");
        OffLightPolice = serializedObject.FindProperty("OffLightPolice");
    }

    void DrawCustomInspector()
    {
        designPart = EditorGUILayout.Foldout(designPart, "■ Design Fields", true, headerStyle);
        if (designPart)
        {
            EditorGUILayout.PropertyField(fadeTime);
            EditorGUILayout.PropertyField(threshold);
            EditorGUILayout.PropertyField(rayLength);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(maxTappedCar);
            EditorGUILayout.PropertyField(maxAllowedCar);
            EditorGUILayout.PropertyField(dragTimeScale);
        }

        otherPart = EditorGUILayout.Foldout(otherPart, "■ Other Fields", true, headerStyle);
        if (otherPart)
        {
            EditorGUILayout.PropertyField(tappedSlider);
            EditorGUILayout.PropertyField(garage);
            EditorGUILayout.PropertyField(whiteText);
            EditorGUILayout.PropertyField(blueText);
            EditorGUILayout.PropertyField(OffLightPolice);
            EditorGUILayout.PropertyField(Lights);
            EditorGUILayout.PropertyField(uiManager);
            //EditorGUILayout.PropertyField(BotUIPanel);
        }        
    }
}
