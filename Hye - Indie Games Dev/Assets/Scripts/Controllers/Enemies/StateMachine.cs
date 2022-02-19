using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : BaseState
{
    private Dictionary<Type, T> availableStates;

    public T currentState { get; private set; }
    public event Action<T> OnStateChanged;

    public virtual void SetStates(Dictionary<Type, T> states, Type state)
    {
        availableStates = states;
        currentState = availableStates[state];
    }

    public void Tick()
    {
        Type nextState = currentState?.Tick();

        if (nextState != null &&
            nextState != currentState?.GetType())
        {
            SwitchState(nextState);
        }
    }

    public void SwitchState(Type nextState)
    {
        currentState = availableStates[nextState];
        currentState.OnSwitch();
        OnStateChanged?.Invoke(currentState);
    }

}


