using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


[CustomEditor(typeof(T_JobManager))]
public class JobManagerEditor : Editor
{
    //fields
    SerializedProperty policePanel;
    SerializedProperty h_Circle;
    SerializedProperty pointer;
    SerializedProperty slider;
    SerializedProperty arrayIndex;
    SerializedProperty rayBlocker;
    SerializedProperty type;

    //reorderable part
    SerializedProperty jobArray;
    ReorderableList jobOrderableList;
    GUIStyle headerStyle = new GUIStyle();
    int totalHeightRatio;



    private void OnEnable()
    {
        headerStyle.fontStyle = FontStyle.BoldAndItalic;

        DefineSerializedProperties();

        jobOrderableList = new ReorderableList(serializedObject, jobArray, true, true, true, true);

        jobOrderableList.drawElementCallback = DrawListElements;
        jobOrderableList.drawHeaderCallback = DrawHeader;
        jobOrderableList.elementHeightCallback = CalculateHeight;
    }
   
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawFields();
        // do orderable list layout jobs
        jobOrderableList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }

    void DefineSerializedProperties()
    {
        arrayIndex = serializedObject.FindProperty("arrayIndex");
        jobArray = serializedObject.FindProperty("t_jobWorks");
        policePanel = serializedObject.FindProperty("policePanel");
        h_Circle = serializedObject.FindProperty("circleHighlighte");
        pointer = serializedObject.FindProperty("handPointer");
        slider = serializedObject.FindProperty("sliderHighlighte");
        rayBlocker = serializedObject.FindProperty("rayBlocker");
        type = serializedObject.FindProperty("tutorialType");
    }

    void DrawFields()
    {
        EditorGUILayout.PropertyField(type);
        EditorGUILayout.PropertyField(policePanel);
        EditorGUILayout.PropertyField(h_Circle);
        EditorGUILayout.PropertyField(pointer);
        EditorGUILayout.PropertyField(slider);
        EditorGUILayout.PropertyField(rayBlocker);
    }

    void DrawListElements(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = jobOrderableList.serializedProperty.GetArrayElementAtIndex(index);

        //general Properties
        SerializedProperty enumField = element.FindPropertyRelative("jobWork");
        SerializedProperty delay = element.FindPropertyRelative("delay");
        SerializedProperty isSimu = element.FindPropertyRelative("isSimultaneous");
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight), enumField, GUIContent.none);

        switch (enumField.enumValueIndex)
        {               
            case 0:
                totalHeightRatio = 1;
                SerializedProperty policeInitializeTxt = element.FindPropertyRelative("PoliceTxt");

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 70, EditorGUIUtility.singleLineHeight), "Delay");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), delay, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 100, EditorGUIUtility.singleLineHeight), "Init Text");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 100 , EditorGUIUtility.singleLineHeight), policeInitializeTxt, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 100, EditorGUIUtility.singleLineHeight), "Simultaneous");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), isSimu, GUIContent.none);
                totalHeightRatio++;
                break;

            case 1:
                break;

            case 2:
                totalHeightRatio = 1;
                SerializedProperty PoliceText = element.FindPropertyRelative("PoliceTxt");

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 100, EditorGUIUtility.singleLineHeight), "P_Text");
                EditorGUI.PropertyField(new Rect(rect.x + 100 , rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight , 100 , EditorGUIUtility.singleLineHeight) , PoliceText , GUIContent.none);
                totalHeightRatio++;
                break;

            case 3:
                SerializedProperty def_ChFadeIn = element.FindPropertyRelative("def_Pos");

                totalHeightRatio = 1;
                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 70, EditorGUIUtility.singleLineHeight), "Delay");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), delay, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 80, EditorGUIUtility.singleLineHeight), "Def Pos");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 120, EditorGUIUtility.singleLineHeight), def_ChFadeIn, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 100, EditorGUIUtility.singleLineHeight), "Simultaneous");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), isSimu, GUIContent.none);
                totalHeightRatio++;
                break;

            case 4:
                totalHeightRatio = 1;
                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 70, EditorGUIUtility.singleLineHeight), "Delay");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), delay, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 100, EditorGUIUtility.singleLineHeight), "Simultaneous");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), isSimu, GUIContent.none);
                totalHeightRatio++;
                break;

            case 5:
                SerializedProperty def_pointerPos = element.FindPropertyRelative("def_Pos");

                totalHeightRatio = 1;
                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 70 , EditorGUIUtility.singleLineHeight), "Delay");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), delay, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 80, EditorGUIUtility.singleLineHeight), "Def Pos");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 120, EditorGUIUtility.singleLineHeight), def_pointerPos, GUIContent.none);
                totalHeightRatio++;                
                break;

            case 6:
                SerializedProperty f_Pos = element.FindPropertyRelative("firstPos");
                SerializedProperty l_Pos = element.FindPropertyRelative("lastPos");
                SerializedProperty f_Transform = element.FindPropertyRelative("firstPosTransform");
                SerializedProperty l_Transform = element.FindPropertyRelative("lastPosTransform");
                SerializedProperty duration = element.FindPropertyRelative("Duration");

                totalHeightRatio = 1;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 70, EditorGUIUtility.singleLineHeight), "Delay");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), delay, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 80, EditorGUIUtility.singleLineHeight), "f_Transform");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 120, EditorGUIUtility.singleLineHeight), f_Transform, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 80, EditorGUIUtility.singleLineHeight), "l_Transform");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 120, EditorGUIUtility.singleLineHeight), l_Transform, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 80, EditorGUIUtility.singleLineHeight), "First Pos");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 120, EditorGUIUtility.singleLineHeight), f_Pos, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 80, EditorGUIUtility.singleLineHeight), "Last Pos");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 120, EditorGUIUtility.singleLineHeight), l_Pos, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 70, EditorGUIUtility.singleLineHeight), "Duration");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 50, EditorGUIUtility.singleLineHeight), duration, GUIContent.none);
                totalHeightRatio++;
                break;

            case 7:
                totalHeightRatio = 1;
                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 70, EditorGUIUtility.singleLineHeight), "Delay");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), delay, GUIContent.none);
                totalHeightRatio++;
                break;

            case 8:
                SerializedProperty t_Scale = element.FindPropertyRelative("timeScale");

                totalHeightRatio = 1;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 70, EditorGUIUtility.singleLineHeight), "Delay");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), delay, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 80, EditorGUIUtility.singleLineHeight), "Time Scale");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 50, EditorGUIUtility.singleLineHeight), t_Scale, GUIContent.none);
                totalHeightRatio++;                
                break;

            case 9:
                SerializedProperty continueState = element.FindPropertyRelative("continueState");

                totalHeightRatio = 1;
                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 70, EditorGUIUtility.singleLineHeight), "C_State");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 120, EditorGUIUtility.singleLineHeight), continueState, GUIContent.none);
                totalHeightRatio++;
                break;

            case 10:
                SerializedProperty def_ShFadeIn = element.FindPropertyRelative("def_Pos");

                totalHeightRatio = 1;
                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 70, EditorGUIUtility.singleLineHeight), "Delay");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), delay, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 80, EditorGUIUtility.singleLineHeight), "Def Pos");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 120, EditorGUIUtility.singleLineHeight), def_ShFadeIn, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 100, EditorGUIUtility.singleLineHeight), "Simultaneous");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), isSimu, GUIContent.none);
                totalHeightRatio++;
                break;

            case 11:
                totalHeightRatio = 1;
                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 70, EditorGUIUtility.singleLineHeight), "Delay");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), delay, GUIContent.none);
                totalHeightRatio++;

                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 100, EditorGUIUtility.singleLineHeight), "Simultaneous");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), isSimu, GUIContent.none);
                totalHeightRatio++;
                break;

            case 12:
                totalHeightRatio = 1;
                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 70, EditorGUIUtility.singleLineHeight), "Delay");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), delay, GUIContent.none);
                totalHeightRatio++;             
                break;

            case 13:
                totalHeightRatio = 1;
                EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 70, EditorGUIUtility.singleLineHeight), "Delay");
                EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y + totalHeightRatio * EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight), delay, GUIContent.none);
                totalHeightRatio++;
                break;
        }
    }

    void DrawHeader(Rect rect)
    {
        string listName = "Tutorial Job Order";
        EditorGUI.LabelField(rect, listName);
    }

    private float CalculateHeight(int index)
    {
        return 7 * EditorGUIUtility.singleLineHeight;
    }
}
