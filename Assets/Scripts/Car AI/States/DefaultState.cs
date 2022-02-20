

public class DefaultState : BaseState
{
    public DefaultState (Vehicle vehicle, _StateMachine stateMachine) : base(vehicle , stateMachine)
    {

    }

    public override void Enter() 
    {
        //vehicle.vehicleState = "default State";
        return;
    }
    public override void LogicUpdate() { return; }
    public override void PhysicsUpdate() { return; }
    public override void Exit() { return; }
    
}
