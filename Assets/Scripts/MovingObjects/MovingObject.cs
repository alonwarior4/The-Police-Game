using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementDirection
{
    None , Right , Left , Up , Down
}

public class MovingObject : MonoBehaviour
{
    #region Variables

    protected MovementDirection moveDir = MovementDirection.None;

    [Header("Waypoints Destination")]
    public Waypoint currentWaypoint;
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public Vector3 destination;
  
    [Header("current speed")]
    public float currentSpeed;

    [HideInInspector] public Animator movingObjectAnim;

    int GoRightAnimId => Animator.StringToHash("M_Right");
    int GoLeftAnimId => Animator.StringToHash("M_Left");
    int GoTopAnimId => Animator.StringToHash("M_Up");
    int GoBotAnimId => Animator.StringToHash("M_Down");
    
    protected Func<float> CalculateDistance;
    protected float _changeWaypointThreshold;

    #endregion



    protected virtual void Awake()
    {
        movingObjectAnim = GetComponent<Animator>();

        SetDestination();
        SetMoveDirection();
        ChangeAnimState();
    }

    protected virtual void Start()
    {
        _changeWaypointThreshold = GameManager.GM_Instance.changeWaypointThreshold;  
    }

    public void SetMoveDirection()
    {
        moveDirection = destination - transform.position;
    }

    public void SetDestination()
    {
        destination = currentWaypoint.transform.position;
    }

    public virtual void Move()
    {
        float distance = CalculateDistance();
        if (distance > _changeWaypointThreshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position, currentSpeed * Time.deltaTime);
        }
        else
        {
            ChangeDestination();
        }
    }

    protected virtual void ChangeDestination() { }

    public void ChangeAnimState()
    {
        float xOffset = moveDirection.x;
        float yOffset = moveDirection.y;
        float absoluteX = Mathf.Abs(xOffset);
        float absoluteY = Mathf.Abs(yOffset);

        if (absoluteX > absoluteY)
        {
            CalculateDistance = HorizontalDistanceCheck;

            if (xOffset > 0)
            {
                movingObjectAnim.SetTrigger(GoRightAnimId);
                moveDir = MovementDirection.Right;
            }
            else
            {
                movingObjectAnim.SetTrigger(GoLeftAnimId);
                moveDir = MovementDirection.Left;
            }
        }
        else
        {
            CalculateDistance = VerticalDistanceCheck;

            if (yOffset > 0)
            {
                movingObjectAnim.SetTrigger(GoTopAnimId);
                moveDir = MovementDirection.Up;
            }
            else
            {
                movingObjectAnim.SetTrigger(GoBotAnimId);
                moveDir = MovementDirection.Down;
            }
        }

        ChangeAnimWork();
    }

    float HorizontalDistanceCheck()
    {
        return Mathf.Abs(destination.x - transform.position.x);
    }

    float VerticalDistanceCheck()
    {
        return Mathf.Abs(destination.y - transform.position.y);
    }

    protected virtual void ChangeAnimWork() { }   
}
