using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BolBolUtility.BolBolEditorTools;

[InitializeOnLoad]
public class WaypointEditor : MonoBehaviour
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void DrawWaypointSystemGizmo(Waypoint waypoint , GizmoType gizmoType)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(waypoint.transform.position, 0.07f);

        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        Handles.Label(waypoint.transform.position, waypoint.wayPointId.ToString(), style);
        

        if (waypoint.nextLines.Count > 0)
        {
            for(int i=0; i < waypoint.nextLines.Count; i++)
            {
                if (waypoint.nextLines[i].nextWaypoint != null)
                {
                    if (waypoint.nextLines[i].IsWrongWay)
                    {
                        Handles.color = Color.red;
                    }
                    else
                    {
                        Handles.color = Color.green;
                    }

                    BolBolTools.DrawArrowLines(3.5f, waypoint.transform.position, waypoint.nextLines[i].nextWaypoint.transform.position, 0.1f, 0.25f, 0.15f , 3f);
                }          
            }
        }
    }

}
