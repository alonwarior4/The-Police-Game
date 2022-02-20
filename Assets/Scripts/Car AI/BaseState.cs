

public class BaseState 
{
    public Vehicle vehicle;
    public _StateMachine stateMachine;

    public BaseState(Vehicle _vehicle , _StateMachine _stateMachine)
    {
        vehicle = _vehicle;
        stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {

    } 

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {
        
    }
}
