using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(Tutorial))]
public class TutorialEditor : Editor
{
    bool /*JobManagerParts , OtherParts ,*/ GeneralJobManagerFields, SignJobManagerFields, SpecialCarJobManagerFields = false;

    //General Fields
    SerializedProperty f_default;
    SerializedProperty f_CrimeCar;
    SerializedProperty f_sideCrime;
    SerializedProperty f_SignMoney;
    SerializedProperty f_CameraMoney;

    //Sign fields
    SerializedProperty f_OneWaySign;
    //SerializedProperty f_NoTurnLeftSign;
    SerializedProperty f_NoTurnRightSign;
    SerializedProperty f_NoTurningSign;
    //SerializedProperty f_NoParkingSign;

    //Special Fields
    SerializedProperty f_NoTapCar;
    SerializedProperty f_MultiTapCar;
    SerializedProperty f_InvisibleCar;

    ////Other Fields
    //SerializedProperty sceneCars;

    GUIStyle headerStyle = new GUIStyle();

    private void OnEnable()
    {
        headerStyle.fontStyle = FontStyle.BoldAndItalic;
        DefineSerializedPrperties();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawCustomInspector();
        serializedObject.ApplyModifiedProperties();
    }

    void DefineSerializedPrperties()
    {
        f_default = serializedObject.FindProperty("defaultTutorial");
        f_sideCrime = serializedObject.FindProperty("m_firstCarSideCrime");
        f_CrimeCar = serializedObject.FindProperty("m_firstCarCrime");
        f_SignMoney = serializedObject.FindProperty("m_firstSignMoney");
        f_CameraMoney = serializedObject.FindProperty("m_firstCameraMoney");

        f_OneWaySign = serializedObject.FindProperty("m_firstTimeOneWaySign");
        //f_NoTurnLeftSign = serializedObject.FindProperty("m_firstTimeNoTurnLeftSign");
        f_NoTurnRightSign = serializedObject.FindProperty("m_firstTimeNoTurnRightSign");
        f_NoTurningSign = serializedObject.FindProperty("m_firstTimeNoTurningSign");
        //f_NoParkingSign = serializedObject.FindProperty("m_firstTimeNoParkingSign");

        f_NoTapCar = serializedObject.FindProperty("m_firstTimeNoTapCar");
        f_MultiTapCar = serializedObject.FindProperty("m_firstTimeMultiTapCar");
        f_InvisibleCar = serializedObject.FindProperty("m_firstTimeInvisibleCar");

        //sceneCars = serializedObject.FindProperty("sceneCars");
    }

    void DrawCustomInspector()
    {
        //JobManagerParts = EditorGUILayout.Foldout(JobManagerParts, "■ Job Managers", true, headerStyle);
        //if (JobManagerParts)
        //{
        //EditorGUI.indentLevel++;
        GeneralJobManagerFields = EditorGUILayout.Foldout(GeneralJobManagerFields, "■ General Managers", true, headerStyle);
        if (GeneralJobManagerFields)
        {
            EditorGUILayout.PropertyField(f_default);
            EditorGUILayout.PropertyField(f_CrimeCar);
            EditorGUILayout.PropertyField(f_sideCrime);
            EditorGUILayout.PropertyField(f_SignMoney);
            EditorGUILayout.PropertyField(f_CameraMoney);
            EditorGUILayout.Space();
        }

        SignJobManagerFields = EditorGUILayout.Foldout(SignJobManagerFields, "■ Sign Managers", true, headerStyle);
        if (SignJobManagerFields)
        {
            EditorGUILayout.PropertyField(f_OneWaySign);
            //EditorGUILayout.PropertyField(f_NoTurnLeftSign);
            EditorGUILayout.PropertyField(f_NoTurnRightSign);
            EditorGUILayout.PropertyField(f_NoTurningSign);
            //EditorGUILayout.PropertyField(f_NoParkingSign);
            EditorGUILayout.Space();
        }

        SpecialCarJobManagerFields = EditorGUILayout.Foldout(SpecialCarJobManagerFields, "■ Special Managers", true, headerStyle);
        if (SpecialCarJobManagerFields)
        {
            EditorGUILayout.PropertyField(f_NoTapCar);
            EditorGUILayout.PropertyField(f_MultiTapCar);
            EditorGUILayout.PropertyField(f_InvisibleCar);
            EditorGUILayout.Space();
        }
        //}

        //EditorGUI.indentLevel = 0;
        //OtherParts = EditorGUILayout.Foldout(OtherParts , "■ Other" , true , headerStyle);
        //if (OtherParts)
        //{
        //    EditorGUILayout.PropertyField(sceneCars);
        //}
    }
}
