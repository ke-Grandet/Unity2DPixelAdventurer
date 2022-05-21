using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T>
{

    static FSM()
    {
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentException("T������ö������");
        }
    }

    private IState currentState;

    private Dictionary<T, IState> states = new();

    public FSM(Dictionary<T, IState> states, T initStateEnum)
    {
        this.states = states;
        TransitionState(initStateEnum);
    }

    public void OnUpdate()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(T nextStateEnum)
    {
        if (nextStateEnum == null)
        {
            return;
        }
        // ������ǰ״̬
        if (currentState != null)
        {
            currentState.OnExit();
        }
        // ������һ״̬
        currentState = states[nextStateEnum];
        currentState.OnEnter();
    }

}
