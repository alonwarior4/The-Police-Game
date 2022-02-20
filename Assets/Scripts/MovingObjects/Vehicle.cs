using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingObjectCrimeState
{
    None , SideCrime , MainCrime
}

[SelectionBase]
[RequireComponent(typeof(Animator) , typeof(BoxCollider2D) , typeof(Rigidbody2D))]
public class Vehicle : MovingObject 
{
    #region Variables        
    public Waypoint nextWaypoint;
    //public string vehicleState;
    //public string stopCause;

    [Header("maximum speed allowed")]
    public float maxSpeed;
    [Header("accelerates")]
    public float moveAccelerate;
    public float stopAccelerate;

    [HideInInspector] public _StateMachine moveSM;
    [HideInInspector] public MoveState movingState;
    [HideInInspector] public StopState stopingState;
    [HideInInspector] public BrakeState brakingState;
    [HideInInspector] public DefaultState defaultState;

    [Header("BoxCollider size Control Sprites")]
    [SerializeField] Sprite leftAndRightSprite;
    [SerializeField] Sprite topSprite;
    [SerializeField] Sprite botSprite;       

    [HideInInspector] public Collider2D carCollider;

    protected float _rayLength;
    [HideInInspector] public Vector3 raycastStartOffset;  

    #endregion


    protected override void Awake()
    {        
        carCollider = GetComponent<Collider2D>();
        base.Awake();
    }

    protected override void Start()
    {
        SetNextWaypoint();
        base.Start();

        _rayLength = GameManager.GM_Instance.rayLength;

        moveSM = new _StateMachine();
        movingState = new MoveState(this, moveSM);
        stopingState = new StopState(this, moveSM);
        brakingState = new BrakeState(this, moveSM);
        defaultState = new DefaultState(this, moveSM);

        moveSM.IntializeStateMachine(movingState);
    }

    public virtual void SetNextWaypoint()
    {
        nextWaypoint = currentWaypoint._GetNextRightWaypoint();
    }   

    protected override void ChangeDestination()
    {
        if (nextWaypoint)
        {
            currentWaypoint = nextWaypoint;
            SetDestination();
            SetMoveDirection();
            ChangeAnimState();

            SetNextWaypoint();
        }
        else
        {
            moveSM.ChangeState(stopingState);
            //stopCause = "no next waypoint";
        }
    }

    protected virtual void Update()
    {
        moveSM.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        moveSM.currentState.PhysicsUpdate();
    }

    protected override void ChangeAnimWork()
    {
        switch (moveDir)
        {
            case MovementDirection.None:
                break;
            case MovementDirection.Right:
                raycastStartOffset = new Vector3((leftAndRightSprite.bounds.size.x / 2) + 0.1f, 0, 0) * transform.localScale.x;
                break;
            case MovementDirection.Left:
                raycastStartOffset = new Vector3(-(leftAndRightSprite.bounds.size.x / 2 + 0.1f), 0, 0) * transform.localScale.x;
                break;
            case MovementDirection.Up:
                raycastStartOffset = new Vector3(0, (topSprite.bounds.size.y / 2) + 0.1f, 0) * transform.localScale.x;
                break;
            case MovementDirection.Down:
                raycastStartOffset = new Vector3(0, -(botSprite.bounds.size.y / 2 + 0.1f), 0) * transform.localScale.x;
                break;
            default:
                break;
        }
    }

    //TODO : Delete Below Function for Built
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + raycastStartOffset, transform.position + raycastStartOffset + moveDirection.normalized * _rayLength);
    }

    public virtual void TutorialBegins() { moveSM.ChangeState(defaultState); }
    public virtual void TutorialEnds() { moveSM.ChangeState(movingState); }
    
}
