using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerStateController
{
    public IPlayerState state { get; private set; }

    public Dictionary<Type, IPlayerState> states = new Dictionary<Type, IPlayerState>();

    public void Register(IPlayerState state)
    {
        states[state.GetType()] = state;
    }

    public void ChangeState<T>() where T : IPlayerState
    {
        state?.Exit();

        state = states[typeof(T)];
        state.Enter();
    }

    public T GetState<T>() where T : IPlayerState
    {
        return (T)states[typeof(T)];
    }

    public void Update()
    {
        state?.Update();
    }

    public void FixedUpdate()
    {
        state?.FixedUpdate();
    }
}
