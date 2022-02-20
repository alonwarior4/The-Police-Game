using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(Vehicle))]
[CanEditMultipleObjects]
public class VC_Editor : Editor
{
    bool designPart , debugPart , otherPart = false;

    //design fields
    SerializedProperty cur_way;
    SerializedProperty next_way;
    SerializedProperty m_Speed;
    SerializedProperty moveAcc;
    SerializedProperty stopAcc;

    //debug fields
    SerializedProperty cur_speed;

    //other fields
    SerializedProperty side_Sp;
    SerializedProperty top_Sp;
    SerializedProperty bot_Sp;

    GUIStyle headerStyle = new GUIStyle();

    private void OnEnable()
    {
        headerStyle.fontStyle = FontStyle.BoldAndItalic;
        DefineSerializedProperties();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawCustomInpsector();
        serializedObject.ApplyModifiedProperties();
    }

    void DefineSerializedProperties()
    {
        cur_way = serializedObject.FindProperty("currentWaypoint");
        next_way = serializedObject.FindProperty("nextWaypoint");
        m_Speed = serializedObject.FindProperty("maxSpeed");
        moveAcc = serializedObject.FindProperty("moveAccelerate");
        stopAcc = serializedObject.FindProperty("stopAccelerate");

        cur_speed = serializedObject.FindProperty("currentSpeed");        

        side_Sp = serializedObject.FindProperty("leftAndRightSprite");
        top_Sp = serializedObject.FindProperty("topSprite");
        bot_Sp = serializedObject.FindProperty("botSprite");
    }

    void DrawCustomInpsector()
    {
        designPart = EditorGUILayout.Foldout(designPart , "■ Design Fields" , true , headerStyle);
        if (designPart)
        {
            EditorGUILayout.PropertyField(cur_way);
            EditorGUILayout.PropertyField(m_Speed);
            EditorGUILayout.PropertyField(moveAcc);
            EditorGUILayout.PropertyField(stopAcc);
        }

        debugPart = EditorGUILayout.Foldout(debugPart, "■ Debug Fields", true, headerStyle);
        if (debugPart)
        {
            EditorGUILayout.PropertyField(cur_speed);
            EditorGUILayout.PropertyField(next_way);
        }

        otherPart = EditorGUILayout.Foldout(otherPart, "■ Other Fields", true, headerStyle);
        if (otherPart)
        {
            EditorGUILayout.PropertyField(side_Sp);
            EditorGUILayout.PropertyField(top_Sp);
            EditorGUILayout.PropertyField(bot_Sp);
        }
    }
}
