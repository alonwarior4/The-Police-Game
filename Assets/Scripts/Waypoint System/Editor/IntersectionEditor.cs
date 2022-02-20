using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BolBolUtility.BolBolEditorTools;

[InitializeOnLoad]
public class IntersectionEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Pickable | GizmoType.Selected)]
    //public static void OnDrawSceneEnteranceGizmo(I_Enterance enterance , GizmoType type)
    //{
    //    BolBolTools.DrawBoxColliderGizmo(enterance , new Color(1, 0.4f, 0, 0.66f), false);
    //}
    public static void OnDrawSceneEntranceGizmo(InterSection interSection, GizmoType type)
    {
        BolBolTools.DrawBoxColliderGizmo(interSection, new Color(1, 0.4f, 0, 0.66f) , false);
    }
}

//public class OutPutEditor
//{
//    [DrawGizmo(GizmoType.NonSelected | GizmoType.Pickable | GizmoType.Selected)]
//    public static void OnDrawSceneOutputGizmo(I_Exit exit , GizmoType type)
//    {
//        BolBolTools.DrawBoxColliderGizmo(exit, Color.blue * 0.65f, false);
//    }
//}
