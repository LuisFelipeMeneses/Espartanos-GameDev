using UnityEngine;

public class IdleState : IPlayerState
{
    private readonly PlayerActions actions;
    private readonly PlayerStateController stateController;

    public IdleState(PlayerActions actions, PlayerStateController stateController)
    {
        this.actions = actions;
        this.stateController = stateController;
    }

    public void Enter()
    {
        
    }

    public void Update()
    {
        if (actions.Keyboard.Jump.IsPressed())
        {
            stateController.ChangeState<JumpState>();
        }
        else if (actions.Keyboard.Attack.IsPressed())
        {
            stateController.ChangeState<AttackState>();
        }
        else if (actions.Keyboard.Move.ReadValue<float>() != 0)
        {
            stateController.ChangeState<RunState>();
        }
        
    }

    public void FixedUpdate()
    {
        
    }

    public void Exit()
    {
        
    }
}
