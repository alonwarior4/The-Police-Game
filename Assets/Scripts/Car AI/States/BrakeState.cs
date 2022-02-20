using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeState : BaseState
{
    int vehicleLayerMask;
    float objRayLength;
    RaycastHit2D[] results = new RaycastHit2D[2];

    public BrakeState(Vehicle vehicle , _StateMachine _StateMachine) : base (vehicle , _StateMachine)
    {

    }

    public override void Enter()
    {
        //vehicle.vehicleState = "brake state";
        vehicleLayerMask = 1 << 8;
        objRayLength = GameManager.GM_Instance.rayLength;
    }

    public override void LogicUpdate()
    {
        DecreaseSpeed();
        vehicle.Move();
    }

    public override void PhysicsUpdate()
    {
        CheckFrontWithRay();
    }

    private void CheckFrontWithRay()
    {
        if (Physics2D.RaycastNonAlloc(vehicle.transform.position + vehicle.raycastStartOffset, vehicle.moveDirection, results, objRayLength, vehicleLayerMask) == 0)
        {
            stateMachine.ChangeState(vehicle.movingState);
        }
    }

    private void DecreaseSpeed()
    {
        if (vehicle.currentSpeed > 0)
        {
            vehicle.currentSpeed = Mathf.MoveTowards(vehicle.currentSpeed, 0, vehicle.stopAccelerate * Time.deltaTime);
        }
        else
        {
            vehicle.currentSpeed = 0;
        }
    }
}
