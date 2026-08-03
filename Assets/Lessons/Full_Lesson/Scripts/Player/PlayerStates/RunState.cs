using UnityEngine;

public class RunState : IPlayerState
{
    private readonly PlayerActions actions;
    private readonly PlayerMovement movement;
    private readonly PlayerStateController stateController;

    public RunState(PlayerMovement movement, PlayerStateController stateController, PlayerActions actions)
    {
        this.movement = movement;
        this.stateController = stateController;
        this.actions = actions;
    }

    public void Enter()
    {
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if (actions.Keyboard.Attack.IsPressed())
        {
            stateController.ChangeState<AttackState>();
        }
        else if (actions.Keyboard.Move.ReadValue<float>() < 0)
        {
            movement.MoveX(-1);
        }
        else if (actions.Keyboard.Move.ReadValue<float>() > 0)
        {
            movement.MoveX(1);
        }
        else
        {
            stateController.ChangeState<IdleState>();
        }

        if (actions.Keyboard.Jump.IsPressed())
        {
            stateController.ChangeState<JumpState>();
        }
    }

    public void Exit()
    {
        movement.Stop();
    }
}
