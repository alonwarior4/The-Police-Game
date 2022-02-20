
public class _StateMachine
{
    public BaseState currentState;

    public void IntializeStateMachine(BaseState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    } 
}
