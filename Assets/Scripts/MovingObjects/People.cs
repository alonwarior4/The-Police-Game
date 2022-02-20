using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer) , typeof(Animator))]
public class People : MovingObject
{

    protected new void Start()
    {
        _changeWaypointThreshold = 0.05f;
    } 

    protected override void ChangeDestination()
    {
        Waypoint nextWaypoint = currentWaypoint._GetNextRightWaypoint();
        currentWaypoint = nextWaypoint;
        SetDestination();
        SetMoveDirection();
        ChangeAnimState();
    }

    private void Update()
    {
        Move();
    }

}
