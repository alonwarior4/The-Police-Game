using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class VehiclesEditorWindow : EditorWindow
{
    [MenuItem("Tools/Vehicle Editor")]
    public static void OpenVEhicleEditorWindow()
    {
        GetWindow<VehiclesEditorWindow>();
    }

    bool paintMode;
    [SerializeField] List<GameObject> vehiclePrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> crimeCarPrefabs = new List<GameObject>();
    [SerializeField] int prefabIndex;
    string vehiclePath = "Assets/Prefabs/Vehicles/Normal Cars";
    string crimeCarPath = "Assets/Prefabs/Vehicles/Crime Cars";
    Vector3 mousePosition;

    public Transform vehiclesParent;
    public Transform crimeCarsParent;
    public float prefabScale;

    SerializedObject editorObj;

    private void Awake()
    {
        SetRefrences();
    }

    private void OnEnable()
    {
        editorObj = new SerializedObject(this);
        EditorSceneManager.sceneOpened -= SetRefrences;
        EditorSceneManager.sceneOpened += SetRefrences;
    }    

    void SetRefrences(Scene scene = default , OpenSceneMode mode = OpenSceneMode.Single)
    {
        vehiclesParent = GameObject.Find("Cars").transform;
        crimeCarsParent = GameObject.Find("Criminals").transform;
    }

    private void OnGUI()
    {
        EditorGUILayout.PropertyField(editorObj.FindProperty("vehiclesParent"));
        EditorGUILayout.PropertyField(editorObj.FindProperty("crimeCarsParent"));
        EditorGUILayout.PropertyField(editorObj.FindProperty("prefabScale"));
                   
        if(vehiclesParent == null || crimeCarsParent == null)
        {
            EditorGUILayout.HelpBox("Fill Parent Fields first", MessageType.Error);
        }
        else
        {
            EditorGUILayout.HelpBox("Hold down ctrl then click to use normal vehicle prefabs", MessageType.Info);
            paintMode = GUILayout.Toggle(paintMode, "Place Vehicles", "Button", GUILayout.Height(30f));

            List<GUIContent> paletteIcons = new List<GUIContent>();
            foreach (GameObject prefab in crimeCarPrefabs)
            {
                Texture2D texture = AssetPreview.GetAssetPreview(prefab);
                paletteIcons.Add(new GUIContent(texture));
            }

            prefabIndex = GUILayout.SelectionGrid(prefabIndex, paletteIcons.ToArray(), 5);
        }

        editorObj.ApplyModifiedProperties();
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (paintMode && vehiclesParent != null && crimeCarsParent != null)
        {
            DisplayVisualHelp();
            HandleSceneviewInput();
            InstantiatePrefab();
            sceneView.Repaint();
        }
    }

    private void OnFocus()
    {
        //minus one because it subscribe onscenegui multiple time on every focus !!!!!!!!!!!!!!!!!!
        SceneView.duringSceneGui -= OnSceneGUI;
        SceneView.duringSceneGui += OnSceneGUI;
        RefreshPalette();
    }

    private void OnDestroy()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
        EditorSceneManager.sceneOpened -= SetRefrences;
    }

    void DisplayVisualHelp()
    {
        Ray guiRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        mousePosition = guiRay.origin - guiRay.direction * (guiRay.origin.z / guiRay.direction.z);
        Handles.DrawWireCube(mousePosition , new Vector3(0.75f , 0.5f , 0));
    }

    void RefreshPalette()
    {
        vehiclePrefabs.Clear();
        crimeCarPrefabs.Clear();

        string[] vehiclePrefFiles = System.IO.Directory.GetFiles(vehiclePath , "*.prefab");
        for(int i=0; i<vehiclePrefFiles.Length; i++)
        {
            vehiclePrefabs.Add((GameObject)AssetDatabase.LoadAssetAtPath(vehiclePrefFiles[i], typeof(GameObject)));
        }

        string[] crimeCarPrefFiles = System.IO.Directory.GetFiles(crimeCarPath, "*.prefab");
        for(int i=0; i< crimeCarPrefFiles.Length; i++)
        {
            crimeCarPrefabs.Add((GameObject)AssetDatabase.LoadAssetAtPath(crimeCarPrefFiles[i], typeof(GameObject)));
        }
    }

    void HandleSceneviewInput()
    {
        if(Event.current.type == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(0);
        }
    }

    void InstantiatePrefab()
    {        
        if(Event.current.modifiers == EventModifiers.Control && Event.current.type == EventType.MouseDown && Event.current.button == 0 && prefabIndex < vehiclePrefabs.Count)
        {
            GameObject v_PrefabAssetRef = vehiclePrefabs[prefabIndex];
            GameObject v_PrefabObj = PrefabUtility.InstantiatePrefab(v_PrefabAssetRef) as GameObject;
            v_PrefabObj.transform.position = mousePosition;
            v_PrefabObj.transform.localScale = Vector3.one * prefabScale;
            v_PrefabObj.transform.SetParent(vehiclesParent);
            Undo.RegisterCreatedObjectUndo(v_PrefabObj, "");
        }
        else if(Event.current.type == EventType.MouseDown && Event.current.button == 0 && prefabIndex < crimeCarPrefabs.Count)
        {
            GameObject c_PrefabAssetRef = crimeCarPrefabs[prefabIndex];
            GameObject c_PrefabObj = PrefabUtility.InstantiatePrefab(c_PrefabAssetRef) as GameObject;
            c_PrefabObj.transform.position = mousePosition;
            c_PrefabObj.transform.localScale = Vector3.one * prefabScale;
            c_PrefabObj.transform.SetParent(crimeCarsParent);
            Undo.RegisterCreatedObjectUndo(c_PrefabObj, "");
        }
    }
}
