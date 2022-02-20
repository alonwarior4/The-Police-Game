using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopState : BaseState
{
    public StopState(Vehicle vehicle , _StateMachine stateMachine) : base(vehicle , stateMachine)
    {

    }

    public override void Enter()
    {
        //vehicle.vehicleState = "stop state";
    }

    public override void LogicUpdate()
    {
        DecreaseSpeed();
        vehicle.Move();
    }

    private void DecreaseSpeed()
    {
        if(vehicle.currentSpeed == 0) { vehicle.moveSM.ChangeState(vehicle.defaultState); }
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
