using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum JobWorks
{
    PoliceCome , OK_PoliceGo , OK_PoliceChangeTxt , CH_FadeIn , CH_FadeOut , PointerAnim , Drag_Drop , StopHandPointer , TimeScale ,BreakState , SH_FadeIn , SH_FadeOut , BlockRaycast , UnBlockRaycast
}


[System.Serializable]
public class T_Job 
{
    public JobWorks jobWork;
    public ContinueTutoActionState continueState = ContinueTutoActionState.None;

    public float delay;
    public float timeScale = 1;

    //Police State
    //public PoliceTextSO PoliceTxt;
    public Sprite PoliceTxt;

    //Circle highlighte State
    // public Vector2 carPos;
    public Vector2 def_Pos;

    //Pointer State    
    public Transform firstPosTransform;
    public Transform lastPosTransform;
    public Vector2 firstPos;
    public Vector2 lastPos;
    public float Duration;
    //public string triggerName;

    //General bool
    public bool isSimultaneous;
}
