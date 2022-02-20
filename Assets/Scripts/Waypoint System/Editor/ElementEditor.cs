using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class ElementEditor : EditorWindow
{
    [MenuItem("Tools/Element Editor")]
    public static void OpenElementEditor()
    {
        GetWindow<ElementEditor>();
    }

    public Vector2 intersectColliderSize;

    //Refrences
    public Transform IntersectParent;
    public Transform SpawnposParent;
    public Transform signParent;
    public Transform cameraParent;
    SerializedProperty i_ColliderSize;
    //guistyle for headers
    GUIStyle headerStyle = new GUIStyle();

    //Serialize object instance
    SerializedObject elementObj;

    private void Awake()
    {
        SetRefrences();
    }

    private void OnEnable()
    {
        elementObj = new SerializedObject(this);
        headerStyle.fontStyle = FontStyle.BoldAndItalic;
        i_ColliderSize = elementObj.FindProperty("intersectColliderSize");
        EditorSceneManager.sceneOpened -= SetRefrences;
        EditorSceneManager.sceneOpened += SetRefrences;
    }

    private void OnDestroy()
    {
        EditorSceneManager.sceneOpened -= SetRefrences;
    }

    void SetRefrences(Scene scene = default , OpenSceneMode mode = OpenSceneMode.Single)
    {
        IntersectParent = GameObject.Find("Intersects").transform;
        SpawnposParent = GameObject.Find("SpawnPos").transform;
        signParent = GameObject.Find("Signs").transform;
        cameraParent = GameObject.Find("Cameras").transform;
    }

    private void OnGUI()
    {        
        DefineSerializeProperties();

        EditorGUILayout.Space();
        DrawButtons();
        elementObj.ApplyModifiedProperties();
    }

    void DrawButtons()
    {
        if (IntersectParent != null)
            CreateIntersectionButton(); 

        if (SpawnposParent != null)
            CreateSpawnPositionButton();

        if (signParent != null)
            CreateSignButton();

        if (cameraParent != null)
            CreateCamera();

        EditorGUILayout.PropertyField(i_ColliderSize);


        if(IntersectParent == null || SpawnposParent == null || signParent == null || cameraParent == null)
        {
            EditorGUILayout.HelpBox("Set Parent Refrences First", MessageType.Error);
        }
    }

    void DefineSerializeProperties()
    {
        EditorGUILayout.LabelField("Refrences", headerStyle);
        EditorGUILayout.PropertyField(elementObj.FindProperty("IntersectParent"));
        EditorGUILayout.PropertyField(elementObj.FindProperty("SpawnposParent"));
        EditorGUILayout.PropertyField(elementObj.FindProperty("signParent"));
        EditorGUILayout.PropertyField(elementObj.FindProperty("cameraParent"));
    }

    void CreateIntersectionButton()
    {
        if(GUILayout.Button("Create Intersection"))
        {
            bool isSelectedWaypoints = true;            
            GameObject intersectionAssetRef = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Waypoint System/Intersection.prefab", typeof(GameObject));
            GameObject intersectionPrefab =  PrefabUtility.InstantiatePrefab(intersectionAssetRef) as GameObject;

            if(Selection.gameObjects.Length > 0)
            {
                for(int i=0; i< Selection.gameObjects.Length; i++)
                {
                    if (!Selection.gameObjects[i].GetComponent<Waypoint>())
                    {
                        isSelectedWaypoints = false;
                    }
                }
            }

            if (isSelectedWaypoints)
            {
                InterSection intersection = intersectionPrefab.GetComponent<InterSection>();
                for(int i=0 ; i<Selection.gameObjects.Length ; i++)
                {
                    intersection.relatedWaypoint.Add(Selection.gameObjects[i].GetComponent<Waypoint>());
                }
            }

            Vector3 averagePos = Vector3.zero;
            for(int i=0; i< Selection.gameObjects.Length; i++)
            {
                averagePos += Selection.gameObjects[i].transform.position;
            }

            averagePos = averagePos / Selection.gameObjects.Length;

            intersectionPrefab.transform.position = averagePos;
            intersectionPrefab.GetComponent<BoxCollider2D>().size = intersectColliderSize;
            intersectionPrefab.transform.rotation = Quaternion.identity;
            intersectionPrefab.transform.SetParent(IntersectParent);
            intersectionPrefab.name = "Intersection " + IntersectParent.childCount;
            Selection.activeGameObject = intersectionPrefab;
            Undo.RegisterCreatedObjectUndo(intersectionPrefab, "");
        }
    }

    void CreateSpawnPositionButton()
    {
        if(GUILayout.Button("Create SpawnPosition"))
        {
            GameObject spawnPostionAssetRef = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Waypoint System/Spawn Position.prefab", typeof(GameObject));
            GameObject spawnPositionPrefab = PrefabUtility.InstantiatePrefab(spawnPostionAssetRef) as GameObject;            
            spawnPositionPrefab.transform.SetParent(SpawnposParent);
            if (Selection.activeGameObject && Selection.activeGameObject.TryGetComponent(out Waypoint waypoint))
            {
                spawnPositionPrefab.transform.position = Selection.activeGameObject.transform.position;
                spawnPositionPrefab.GetComponent<SpawnPos>().spawnWaypoint = waypoint;
            }
            else
            {
                spawnPositionPrefab.transform.position = Vector3.zero;
            }
            spawnPositionPrefab.name = "Spawn Position " + SpawnposParent.childCount;
            Selection.activeGameObject = spawnPositionPrefab;
            Undo.RegisterCreatedObjectUndo(spawnPositionPrefab , "");
        }
    }

    void CreateSignButton()
    {
        if (GUILayout.Button("Create Sign"))
        {
            GameObject signAssetRef = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Waypoint System/Sign.prefab", typeof(GameObject));
            GameObject signPrefab = PrefabUtility.InstantiatePrefab(signAssetRef) as GameObject;
            signPrefab.transform.SetParent(signParent);
            signPrefab.transform.position = Vector3.zero;
            signPrefab.transform.rotation = Quaternion.identity;
            signPrefab.name = "Sign " + signParent.childCount;
            Selection.activeGameObject = signPrefab;
            Undo.RegisterCreatedObjectUndo(signPrefab , "");
        }
    }

    void CreateCamera()
    {
        if (GUILayout.Button("Create Camera"))
        {
            GameObject cameraAssetRef = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Waypoint System/Camera.prefab", typeof(GameObject));
            GameObject cameraPrefab = PrefabUtility.InstantiatePrefab(cameraAssetRef) as GameObject;
            cameraPrefab.transform.SetParent(cameraParent);
            cameraPrefab.transform.position = Vector3.zero;
            cameraPrefab.transform.rotation = Quaternion.identity;
            cameraPrefab.name = "Camera " + cameraParent.childCount;
            Selection.activeGameObject = cameraPrefab;
            Undo.RegisterCreatedObjectUndo(cameraPrefab , "");
        }
    }
}
