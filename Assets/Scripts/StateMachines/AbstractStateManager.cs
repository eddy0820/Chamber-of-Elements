using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractStateManager : MonoBehaviour
{
    public State currentState;

    private void Update()
    {
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

    protected void SwitchToNextState(State nextState)
    {
        currentState = nextState;
    }
}
