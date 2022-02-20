using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    int vehicleLayerMask;
    float objRayLength;
    RaycastHit2D[] results = new RaycastHit2D[2];


    public MoveState(Vehicle vehicle , _StateMachine stateMachine) : base(vehicle, stateMachine)
    {

    }

    public override void Enter()
    {
        //vehicle.vehicleState = "move state";
        vehicleLayerMask = 1 << 8;
        objRayLength = GameManager.GM_Instance.rayLength;
    }

    public override void LogicUpdate()
    {
        IncreaseSpeed();
        vehicle.Move();
    }

    public override void PhysicsUpdate()
    {
        CheckFrontWithRay();
    }

    private void CheckFrontWithRay()
    {                
        if(Physics2D.RaycastNonAlloc(vehicle.transform.position + vehicle.raycastStartOffset , vehicle.moveDirection , results , objRayLength , vehicleLayerMask) > 0)
        {
            stateMachine.ChangeState(vehicle.brakingState);
        }
    }

    private void IncreaseSpeed()
    {
        if (vehicle.currentSpeed < vehicle.maxSpeed)
        {
            vehicle.currentSpeed = Mathf.MoveTowards(vehicle.currentSpeed, vehicle.maxSpeed, vehicle.moveAccelerate * Time.deltaTime);
        }
        else
        {
            vehicle.currentSpeed = vehicle.maxSpeed;
        }
    }
}
