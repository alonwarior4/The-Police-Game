using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BolBolUtility.BolBolEditorTools;


[InitializeOnLoad]
public class PlacableEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawMainPlaceEditor(MainCrimePlace mainCrimePlace, GizmoType type)
    {
        BolBolTools.DrawBoxColliderGizmo(mainCrimePlace, new Color(0 , 0 , 1f , 0.5f), false);
    }
}


[InitializeOnLoad]
public class CameraColldierGizmoz
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Pickable | GizmoType.Selected)]
    public static void OnDrawCameraCoolliderGizmo(_Camera camera , GizmoType type)
    {
        BolBolTools.DrawBoxColliderGizmo(camera, new Color(0, 0.93f, 1f , 0.6f), false);
    }
}


[InitializeOnLoad]
public class CameraPlaceEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Pickable | GizmoType.Selected)]
    public static void OnDrawCameraPlaceEditor(CameraCrimePlace cameraCrimePlace, GizmoType type)
    {
        BolBolTools.DrawBoxColliderGizmo(cameraCrimePlace, new Color(0, 0.93f, 1f , 0.6f) , false);
    }
}


[InitializeOnLoad]
public class MainSignEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Pickable | GizmoType.Selected)]
    public static void OnDrawMainSignEditor(Sign sign , GizmoType type)
    {
        Gizmos.color = Color.blue * 0.5f;
        Gizmos.DrawWireSphere(sign.lineRendererStartPos.position, 0.1f);

        BolBolTools.DrawBoxColliderGizmo(sign, new Color(0, 0, 1f, 0.5f), false);
    }
}
