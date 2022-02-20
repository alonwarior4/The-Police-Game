using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpecialCar : CrimeCar
{
    List<Waypoint> Path = new List<Waypoint>();    
    [Header("Last waypoint of path")]
    [HideInInspector] public int pathIndex = 0;

    

    protected new void Awake()
    {
        movingObjectAnim = GetComponent<Animator>();        
        carCollider = GetComponent<Collider2D>();
    }

    protected override void Start()
    {
        base.Start();
        moveSM.IntializeStateMachine(defaultState);
    }

    public void FindWaypointPath()
    {
        Path = SpecialCarManager.SCM_Instance.GetSpecialPath(currentWaypoint).pathPoints;
    }    

    protected override void ChangeDestination()
    {
        pathIndex++;
        if (pathIndex < Path.Count)
        {
            currentWaypoint = Path[pathIndex];
            SetDestination();
            SetMoveDirection();
            ChangeAnimState();
        }
        else
        {
            moveSM.ChangeState(stopingState);
        }
    }   

    public override void MainCrimeCollider(Sign sign)
    {
        Waypoint current = Path[pathIndex];
        Waypoint next = Path[pathIndex + 1];
        if (Waypoint.GetLine(current, next).IsWrongWay == false) return;

        currentSign = sign;
        currentSign.AddToMainCrimeCars(this);
        CurrentCrime = CrimeManager.GetCrimeByCrimeType(currentSign.signCrimeType, this);
        isCanTouch = true;
    }

    public virtual void DoSomeThingBeforeResapwn() { }

    //TODO : change function when tutorial ends
    public override void TutorialEnds() { return; }

    public virtual void CheckSpecTutorial() { }    
    
}
