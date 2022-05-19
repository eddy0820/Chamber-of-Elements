using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractStateManager : MonoBehaviour
{
    [ReadOnly] public State currentState;

    private void Update()
    {
        OnUpdate();
        RunStateMachine();
    }

    protected virtual void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if (nextState != currentState && nextState != null)
        {
            SwitchToNextState(nextState);
        }
    }

    protected virtual void SwitchToNextState(State nextState)
    {   
        if(currentState != null)
        {
            currentState.OnExitState();
        }
        
        currentState = nextState;
        currentState.OnEnterState();
    }

    protected virtual void OnUpdate() {}
}
