using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using BolBolUtility.BolBolEditorTools;


[InitializeOnLoad]
public class SpecEnterEditor : Editor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Pickable | GizmoType.Selected)]
    public static void DrawSpecEnterCollider(SpecEnterTrigger specEnter , GizmoType type)
    {
        BolBolTools.DrawBoxColliderGizmo(specEnter, Color.magenta * 0.6f, false);
    }
}
