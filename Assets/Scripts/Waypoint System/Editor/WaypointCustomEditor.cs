using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;




[InitializeOnLoad]
public class WaypointCustomEditor : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void OpenWindow()
    {
        GetWindow<WaypointCustomEditor>();
    }

    public Transform waypointRoot;

    private void Awake()
    {
        SetRefrences();
    }

    private void OnEnable()
    {
        EditorSceneManager.sceneOpened -= SetRefrences;
        EditorSceneManager.sceneOpened += SetRefrences;
    }

    private void OnDestroy()
    {
        EditorSceneManager.sceneOpened -= SetRefrences;
    }

    private void OnGUI()
    {             
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if (waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Please Select WaypointRoot From Hirearchy", MessageType.Error);
        }
        else
        {
            EditorGUILayout.BeginVertical();
            DrawButtons();
            ShowTips();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();       
    }

    void SetRefrences(Scene scene = default , OpenSceneMode mode = OpenSceneMode.Single)
    {        
        waypointRoot = GameObject.Find("Ways").transform;
    }

    void DrawButtons()
    {
        EditorGUILayout.BeginHorizontal();
        CreateRightWaypoint();
        CreateWrongWaypoint();
        EditorGUILayout.EndHorizontal();
        CreateWaypointInBetweenButton();
        if (Selection.activeGameObject && Selection.activeGameObject.GetComponent<Waypoint>() != null)
        {
            RemoveWaypointButton();
        }

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        CreateRightLine();
        CreateWrongLine();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        FixXPos();
        FixYPos();
        EditorGUILayout.EndHorizontal();
        RemoveLinkBetweenTwoSelectedWaypoint();
        ChangeLineType();
        ReverseLineDirection();       
    }

    void FixXPos()
    {
        if (GUILayout.Button("xPos Fix"))
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;
            for (int i = 0; i < selectedGameObjects.Length; i++)
            {
                if (!selectedGameObjects[i].GetComponent<Waypoint>())
                {
                    return;
                }
            }

            float averageXPos = 0;
            for (int i = 0; i < selectedGameObjects.Length; i++)
            {
                averageXPos += selectedGameObjects[i].transform.localPosition.x;
            }

            averageXPos /= selectedGameObjects.Length;

            for (int i = 0; i < selectedGameObjects.Length; i++)
            {
                Vector3 newPos = selectedGameObjects[i].transform.localPosition;
                newPos.x = averageXPos;
                selectedGameObjects[i].transform.localPosition = newPos;
            }
        }       
    }

    void FixYPos()
    {
        if (GUILayout.Button("yPos Fix"))
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;
            for (int i = 0; i < selectedGameObjects.Length; i++)
            {
                if (!selectedGameObjects[i].GetComponent<Waypoint>())
                {
                    return;
                }
            }

            float averageYPos = 0;
            for (int i = 0; i < selectedGameObjects.Length; i++)
            {
                averageYPos += selectedGameObjects[i].transform.localPosition.y;
            }

            averageYPos /= selectedGameObjects.Length;

            for (int i = 0; i < selectedGameObjects.Length; i++)
            {
                Vector3 newPos = selectedGameObjects[i].transform.localPosition;
                newPos.y = averageYPos;
                selectedGameObjects[i].transform.localPosition = newPos;
            }
        }
    }

    void ShowTips()
    {
        EditorGUILayout.HelpBox("Use remove waypoint button wisely , if waypoint has one in and one out , use it , else use remove line", MessageType.Warning);
        EditorGUILayout.HelpBox("For create Line or remove Line , Select 2 waypoint in order to specify direction" , MessageType.Warning);
        EditorGUILayout.HelpBox("For Create Middle Waypoint Select 2 Continuous waypoint", MessageType.Info);
        EditorGUILayout.HelpBox("Select a waypoint and click create waypoint to auto connect next waypoint", MessageType.Info);        
    }

    void CreateRightWaypoint()
    {
        if (GUILayout.Button("Create R_Waypoint"))
        {
            GameObject newObj = new GameObject("Waypoint", typeof(Waypoint));
            newObj.transform.parent = waypointRoot;

            Waypoint newWaypoint = newObj.GetComponent<Waypoint>();
            newWaypoint.wayPointId = newWaypoint.transform.GetSiblingIndex() + 1;

            newObj.name = "Waypoint " + newWaypoint.wayPointId;

            if (Selection.activeGameObject &&  Selection.activeGameObject.GetComponent<Waypoint>() != null)
            {
                Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

                nextWaypointLines newLine = new nextWaypointLines();
                newLine.nextWaypoint = newWaypoint;

                selectedWaypoint.nextLines.Add(newLine);

                newWaypoint.previousWaypoints.Add(selectedWaypoint);
                newWaypoint.transform.position = selectedWaypoint.transform.position + selectedWaypoint.transform.up.normalized * 0.5f;

                for(int i=0; i< selectedWaypoint.nextLines.Count; i++)
                {
                    selectedWaypoint.nextLines[i].waypointChance = 1f / selectedWaypoint.nextLines.Count;
                }
            }

            Selection.activeGameObject = newObj;
        }
    }

    void CreateWrongWaypoint()
    {
        if (GUILayout.Button("Create W_Waypoint"))
        {
            GameObject newObj = new GameObject("Waypoint", typeof(Waypoint));
            newObj.transform.parent = waypointRoot;

            Waypoint newWaypoint = newObj.GetComponent<Waypoint>();
            newWaypoint.wayPointId = newWaypoint.transform.GetSiblingIndex() + 1;

            newObj.name = "Waypoint " + newWaypoint.wayPointId;

            if (Selection.activeGameObject && Selection.activeGameObject.GetComponent<Waypoint>() != null)
            {
                Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

                nextWaypointLines newLine = new nextWaypointLines();
                newLine.IsWrongWay = true;
                newLine.nextWaypoint = newWaypoint;

                selectedWaypoint.nextLines.Add(newLine);

                newWaypoint.previousWaypoints.Add(selectedWaypoint);
                newWaypoint.transform.position = selectedWaypoint.transform.position + selectedWaypoint.transform.up.normalized * 0.5f;

                for (int i = 0; i < selectedWaypoint.nextLines.Count; i++)
                {
                    selectedWaypoint.nextLines[i].waypointChance = 1f / selectedWaypoint.nextLines.Count;
                }
            }

            Selection.activeGameObject = newObj;
        }
    }

    void CreateWaypointInBetweenButton()
    {
        if (GUILayout.Button("Create Middle Waypoint"))
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            if (selectedObjects.Length == 2)
            {
                for (int i = 0; i < selectedObjects.Length; i++)
                {
                    if (!selectedObjects[i].transform.GetComponent<Waypoint>())
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }

            Waypoint selectedWaypoint1 = selectedObjects[0].transform.GetComponent<Waypoint>();
            Waypoint selectedWaypoint2 = selectedObjects[1].transform.GetComponent<Waypoint>();
            List<Waypoint> waypoint1NextWaypoints = new List<Waypoint>();
            List<Waypoint> waypoint2NextWaypoints = new List<Waypoint>();

            if (!selectedWaypoint1.previousWaypoints.Contains(selectedWaypoint2) && !selectedWaypoint2.previousWaypoints.Contains(selectedWaypoint1))
                return;

            GameObject newObj = new GameObject("Waypoint", typeof(Waypoint));
            newObj.transform.parent = waypointRoot;

            Waypoint newWaypoint = newObj.GetComponent<Waypoint>();
            newWaypoint.wayPointId = newWaypoint.transform.GetSiblingIndex() + 1;

            newObj.name = "Waypoint " + newWaypoint.wayPointId;            

            if (selectedWaypoint1.nextLines.Count > 0)
            {
                for (int i = 0; i < selectedWaypoint1.nextLines.Count; i++)
                {
                    waypoint1NextWaypoints.Add(selectedWaypoint1.nextLines[i].nextWaypoint);
                }
            }

            if (selectedWaypoint2.nextLines.Count > 0)
            {
                for (int i = 0; i < selectedWaypoint2.nextLines.Count; i++)
                {
                    waypoint2NextWaypoints.Add(selectedWaypoint2.nextLines[i].nextWaypoint);
                }
            }

            if (waypoint1NextWaypoints.Count > 0 && waypoint1NextWaypoints.Contains(selectedWaypoint2))
            {
                nextWaypointLines newLine = new nextWaypointLines();
                newLine.nextWaypoint = selectedWaypoint2;
                newWaypoint.nextLines.Add(newLine);
                for (int i = 0; i < selectedWaypoint1.nextLines.Count; i++)
                {
                    if (selectedWaypoint1.nextLines[i].nextWaypoint == selectedWaypoint2)
                    {
                        selectedWaypoint1.nextLines.Remove(selectedWaypoint1.nextLines[i]);
                        nextWaypointLines _newLine = new nextWaypointLines();
                        _newLine.nextWaypoint = newWaypoint;
                        selectedWaypoint1.nextLines.Add(_newLine);

                        for(int j=0 ; j< selectedWaypoint1.nextLines.Count; j++)
                        {
                            selectedWaypoint1.nextLines[j].waypointChance = 1f / selectedWaypoint1.nextLines.Count;
                        }
                    }
                }

                selectedWaypoint2.previousWaypoints.Remove(selectedWaypoint1);
                selectedWaypoint2.previousWaypoints.Add(newWaypoint);
                newWaypoint.previousWaypoints.Add(selectedWaypoint1);
            }
            else if (waypoint2NextWaypoints.Count > 0 && waypoint2NextWaypoints.Contains(selectedWaypoint1))
            {
                nextWaypointLines newLine = new nextWaypointLines();
                newLine.nextWaypoint = selectedWaypoint1;
                newWaypoint.nextLines.Add(newLine);

                for (int i = 0; i < selectedWaypoint2.nextLines.Count; i++)
                {
                    if (selectedWaypoint2.nextLines[i].nextWaypoint == selectedWaypoint1)
                    {
                        selectedWaypoint2.nextLines.Remove(selectedWaypoint2.nextLines[i]);
                        nextWaypointLines _newLine = new nextWaypointLines();
                        _newLine.nextWaypoint = newWaypoint;
                        selectedWaypoint2.nextLines.Add(_newLine);

                        for(int j =0; j< selectedWaypoint2.nextLines.Count; j++)
                        {
                            selectedWaypoint2.nextLines[j].waypointChance = 1f / selectedWaypoint2.nextLines.Count;
                        }
                    }
                }

                selectedWaypoint1.previousWaypoints.Remove(selectedWaypoint2);
                selectedWaypoint1.previousWaypoints.Add(newWaypoint);
                newWaypoint.previousWaypoints.Add(selectedWaypoint2);
            }

            Vector3 middlePos = new Vector3((selectedWaypoint1.transform.position.x + selectedWaypoint2.transform.position.x) / 2,
                (selectedWaypoint1.transform.position.y + selectedWaypoint2.transform.position.y) / 2, 0);
            newWaypoint.nextLines[0].waypointChance = 1f;
            newWaypoint.transform.position = middlePos;
            Selection.activeGameObject = newWaypoint.gameObject;
        }
    }

    void CreateRightLine()
    {
        if (GUILayout.Button("Create R_Line"))
        {
            Waypoint firstWaypoint = null;
            Waypoint secondWaypoint = null;
            GameObject[] selectedObjects = Selection.gameObjects;

            if (selectedObjects.Length == 2)
            {
                for (int i = 0; i < selectedObjects.Length; i++)
                {
                    if (!selectedObjects[i].GetComponent<Waypoint>())
                    {
                        return;
                    }
                }

                if (Selection.activeGameObject == selectedObjects[0])
                {
                    firstWaypoint = selectedObjects[1].GetComponent<Waypoint>();
                    secondWaypoint = selectedObjects[0].GetComponent<Waypoint>();
                }
                else if (Selection.activeGameObject == selectedObjects[1])
                {
                    firstWaypoint = selectedObjects[0].GetComponent<Waypoint>();
                    secondWaypoint = selectedObjects[1].GetComponent<Waypoint>();
                }
            }
            else
            {
                return;
            }
           
            CreateLine(firstWaypoint, secondWaypoint , false);
        }
    }

    void CreateWrongLine()
    {
        if (GUILayout.Button("Create W_Line"))
        {
            Waypoint firstWaypoint = null;
            Waypoint secondWaypoint = null;
            GameObject[] selectedObjects = Selection.gameObjects;

            if (selectedObjects.Length == 2)
            {
                for (int i = 0; i < selectedObjects.Length; i++)
                {
                    if (!selectedObjects[i].GetComponent<Waypoint>())
                    {
                        return;
                    }
                }

                if (Selection.activeGameObject == selectedObjects[0])
                {
                    firstWaypoint = selectedObjects[1].GetComponent<Waypoint>();
                    secondWaypoint = selectedObjects[0].GetComponent<Waypoint>();
                }
                else if (Selection.activeGameObject == selectedObjects[1])
                {
                    firstWaypoint = selectedObjects[0].GetComponent<Waypoint>();
                    secondWaypoint = selectedObjects[1].GetComponent<Waypoint>();
                }
            }
            else
            {
                return;
            }

            CreateLine(firstWaypoint, secondWaypoint , true);
        }
    }

    void CreateLine(Waypoint firstWaypoint, Waypoint secondWaypoint , bool isWrongLine)
    {
        for (int i = 0; i < firstWaypoint.nextLines.Count; i++)
        {
            if (firstWaypoint.nextLines[i].nextWaypoint == secondWaypoint)
                return;
        }

        if (secondWaypoint.previousWaypoints.Contains(firstWaypoint))
            return;

        nextWaypointLines nextLine = new nextWaypointLines();
        if (isWrongLine)
        {
            nextLine.IsWrongWay = true;
        }
        else
        {
            nextLine.IsWrongWay = false;
        }
        nextLine.nextWaypoint = secondWaypoint;

        firstWaypoint.nextLines.Add(nextLine);
        secondWaypoint.previousWaypoints.Add(firstWaypoint);

        for (int i = 0; i < firstWaypoint.nextLines.Count; i++)
        {
            firstWaypoint.nextLines[i].waypointChance = 1f / firstWaypoint.nextLines.Count;
        }
    }

    void RemoveLinkBetweenTwoSelectedWaypoint()
    {
        if (GUILayout.Button("Remove Line"))
        {
            Waypoint selectedWaypoint1;
            Waypoint selectedWaypoint2;
            GameObject[] selectedObjects = Selection.gameObjects;
            if (selectedObjects.Length == 2)
            {
                for (int i = 0; i < selectedObjects.Length; i++)
                {
                    if (!selectedObjects[i].transform.GetComponent<Waypoint>())
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }

            selectedWaypoint1 = selectedObjects[0].GetComponent<Waypoint>();
            selectedWaypoint2 = selectedObjects[1].GetComponent<Waypoint>();
            
            RemoveLine(selectedWaypoint1, selectedWaypoint2);
        }
    }

    void RemoveLine(Waypoint selectedWaypoint1, Waypoint selectedWaypoint2)
    {
        if (!selectedWaypoint2.previousWaypoints.Contains(selectedWaypoint1) && !selectedWaypoint1.previousWaypoints.Contains(selectedWaypoint2))
            return;
        nextWaypointLines betweenLine;
        if (selectedWaypoint2.previousWaypoints.Contains(selectedWaypoint1))
        {
            betweenLine = Waypoint.GetLine(selectedWaypoint1, selectedWaypoint2);
            selectedWaypoint1.nextLines.Remove(betweenLine);
            selectedWaypoint2.previousWaypoints.Remove(selectedWaypoint1);

            for (int i = 0; i < selectedWaypoint1.nextLines.Count; i++)
            {
                selectedWaypoint1.nextLines[i].waypointChance = 1f / selectedWaypoint1.nextLines.Count;
            }
        }
        else if (selectedWaypoint1.previousWaypoints.Contains(selectedWaypoint2))
        {
            betweenLine = Waypoint.GetLine(selectedWaypoint2, selectedWaypoint1);
            selectedWaypoint2.nextLines.Remove(betweenLine);
            selectedWaypoint1.previousWaypoints.Remove(selectedWaypoint2);

            for (int i = 0; i < selectedWaypoint2.nextLines.Count; i++)
            {
                selectedWaypoint2.nextLines[i].waypointChance = 1f / selectedWaypoint2.nextLines.Count;
            }
        }
    }

    void RemoveWaypointButton()
    {
        if (GUILayout.Button("Remove Waypoint"))
        {
            Waypoint waypointToRemove = Selection.activeGameObject.GetComponent<Waypoint>();

            List<Waypoint> previousWaypoints = waypointToRemove.previousWaypoints;
            List<nextWaypointLines> nextLines = waypointToRemove.nextLines;

            for (int i = 0; i < previousWaypoints.Count; i++)
            {
                for (int j = 0; j < previousWaypoints[i].nextLines.Count; j++)
                {
                    if (previousWaypoints[i].nextLines[j].nextWaypoint == waypointToRemove)
                    {
                        previousWaypoints[i].nextLines.Remove(previousWaypoints[i].nextLines[j]);
                    }
                }

                if (nextLines.Count == 1)
                {
                    if (!previousWaypoints[i].nextLines.Contains(nextLines[0]))
                    {
                        previousWaypoints[i].nextLines.Add(nextLines[0]);
                    }
                    if (!nextLines[0].nextWaypoint.previousWaypoints.Contains(previousWaypoints[i]))
                    {
                        nextLines[0].nextWaypoint.previousWaypoints.Add(previousWaypoints[i]);
                    }

                    for (int j = 0; j < previousWaypoints[i].nextLines.Count; j++)
                    {
                        previousWaypoints[i].nextLines[j].waypointChance = 1f / previousWaypoints[i].nextLines.Count;
                    }
                }                
            }

            for (int i = 0; i < nextLines.Count; i++)
            {
                nextLines[i].nextWaypoint.previousWaypoints.Remove(waypointToRemove);

                if (previousWaypoints.Count == 1)
                {
                    if (!nextLines[i].nextWaypoint.previousWaypoints.Contains(previousWaypoints[0]))
                    {
                        nextLines[i].nextWaypoint.previousWaypoints.Add(previousWaypoints[0]);
                    }
                    if (!previousWaypoints[0].nextLines.Contains(nextLines[i]))
                    {
                        previousWaypoints[0].nextLines.Add(nextLines[i]);
                    }

                    for (int j = 0; j < previousWaypoints[0].nextLines.Count; j++)
                    {
                        previousWaypoints[0].nextLines[j].waypointChance = 1f / previousWaypoints[0].nextLines.Count;
                    }
                }
            }

            DestroyImmediate(waypointToRemove.gameObject);

            foreach (Transform child in waypointRoot.transform)
            {
                child.transform.GetComponent<Waypoint>().wayPointId = child.GetSiblingIndex() + 1;
                child.name = "Waypoint " + (child.GetSiblingIndex() + 1).ToString();
            }
        }
    }

    void ReverseLineDirection()
    {
        if(GUILayout.Button("Reverese Line Direction"))
        {
            bool is1To2 = false;
            bool is2To1 = false;
            bool isWrongLine = false;

            GameObject[] selectedObjects = Selection.gameObjects;
            if (selectedObjects.Length == 2)
            {
                for (int i = 0; i < selectedObjects.Length; i++)
                {
                    if (!selectedObjects[i].transform.GetComponent<Waypoint>())
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }

            Waypoint selectedWaypoint1 = selectedObjects[0].GetComponent<Waypoint>();
            Waypoint selectedWaypoint2 = selectedObjects[1].GetComponent<Waypoint>();

            if (selectedWaypoint1.previousWaypoints.Contains(selectedWaypoint2))
            {
                is2To1 = true;
                is1To2 = false;
                isWrongLine = Waypoint.GetLine(selectedWaypoint2 ,selectedWaypoint1).IsWrongWay;
            }
            else if (selectedWaypoint2.previousWaypoints.Contains(selectedWaypoint1))
            {
                is1To2 = true;
                is2To1 = false;
                isWrongLine = Waypoint.GetLine(selectedWaypoint1, selectedWaypoint2).IsWrongWay;
            }

            RemoveLine(selectedWaypoint1 , selectedWaypoint2);

            if (is1To2)
            {
                CreateLine(selectedWaypoint2 , selectedWaypoint1 , isWrongLine);
            }
            else if (is2To1)
            {
                CreateLine(selectedWaypoint1 , selectedWaypoint2 , isWrongLine);
            }
        }
    }

    void ChangeLineType()
    {
        if(GUILayout.Button("Change Line Type"))
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            if (selectedObjects.Length == 2)
            {
                for (int i = 0; i < selectedObjects.Length; i++)
                {
                    if (!selectedObjects[i].transform.GetComponent<Waypoint>())
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }

            Waypoint selectedWaypoint1 = selectedObjects[0].GetComponent<Waypoint>();
            Waypoint selectedWaypoint2 = selectedObjects[1].GetComponent<Waypoint>();

            if (selectedWaypoint1.previousWaypoints.Contains(selectedWaypoint2))
            {
                nextWaypointLines line =  Waypoint.GetLine(selectedWaypoint2, selectedWaypoint1);
                if (line.IsWrongWay)
                {
                    line.IsWrongWay = false;
                }
                else
                {
                    line.IsWrongWay = true;
                }
            }
            else if (selectedWaypoint2.previousWaypoints.Contains(selectedWaypoint1))
            {
                nextWaypointLines line = Waypoint.GetLine(selectedWaypoint1 , selectedWaypoint2);
                if (line.IsWrongWay)
                {
                    line.IsWrongWay = false;
                }
                else
                {
                    line.IsWrongWay = true;
                }
            }
        }
    }    
}
