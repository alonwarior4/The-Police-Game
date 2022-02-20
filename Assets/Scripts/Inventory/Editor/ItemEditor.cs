using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(Item))]
[CanEditMultipleObjects]
public class ItemEditor : Editor
{
    bool designPart, otherPart = false;

    //Design Fields
    SerializedProperty i_Type;
    SerializedProperty i_value;

    //Other Fields
    SerializedProperty valueItem;
    SerializedProperty valueBG_blue;
    SerializedProperty valueBG_gray;
    SerializedProperty numColorBlue; 
    SerializedProperty numColorGray;    

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
        i_Type = serializedObject.FindProperty("itemType");
        i_value = serializedObject.FindProperty("value");

        valueItem = serializedObject.FindProperty("itemValue");
        valueBG_blue = serializedObject.FindProperty("blueValueBG");
        valueBG_gray = serializedObject.FindProperty("grayValueBG");
        numColorBlue = serializedObject.FindProperty("numBlueColor");
        numColorGray = serializedObject.FindProperty("numGrayColor");
    }

    void DrawCustomInspector()
    {
        designPart = EditorGUILayout.Foldout(designPart, "■ Design Fields", true, headerStyle);
        if (designPart)
        {
            EditorGUILayout.PropertyField(i_Type);
            EditorGUILayout.PropertyField(i_value);            
        }

        otherPart = EditorGUILayout.Foldout(otherPart, "■ Other Fields", true, headerStyle);
        if (otherPart)
        {
            EditorGUILayout.PropertyField(valueItem);
            EditorGUILayout.PropertyField(valueBG_blue);
            EditorGUILayout.PropertyField(valueBG_gray);
            EditorGUILayout.PropertyField(numColorBlue);
            EditorGUILayout.PropertyField(numColorGray);
        }           
    }
}
