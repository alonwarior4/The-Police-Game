using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BolBolUtility.BolBolEditorTools;

[InitializeOnLoad]
public class SpawnpointEditor 
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Pickable | GizmoType.Selected)]
    public static void OnDrawSceneSpawnPointEditor(SpawnPos spawnPos , GizmoType type)
    {
        BolBolTools.DrawBoxColliderGizmo(spawnPos, new Color(1 , 1, 1, 0.45f) , false);

        Handles.color = Color.black;
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;

        if (spawnPos.spawnWaypoint)
        {
            Vector2 position = (Vector2)spawnPos.transform.position + new Vector2(spawnPos.GetComponent<BoxCollider2D>().offset.x * spawnPos.transform.localScale.x,
                spawnPos.GetComponent<BoxCollider2D>().offset.y * spawnPos.transform.localScale.y);
            Handles.Label(position , spawnPos.spawnWaypoint.wayPointId.ToString(), style);
        }
    }
}
