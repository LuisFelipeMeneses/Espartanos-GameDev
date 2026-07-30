using UnityEngine;

public class JumpState : IPlayerState
{
    readonly PlayerActions actions;
    readonly PlayerMovement movement;
    readonly PlayerStateController stateController;

    public JumpState(PlayerMovement movement, PlayerStateController stateController, PlayerActions actions)
    {
        this.movement = movement;
        this.stateController = stateController;
        this.actions = actions;
    }

    public void Enter()
    {
        if (movement.CheckGrounded())
        {
            movement.Jump();
        }
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if (actions.Keyboard.Move.ReadValue<float>() < 0)
        {
            movement.MoveX(-1);
        }
        else if (actions.Keyboard.Move.ReadValue<float>() > 0)
        {
            movement.MoveX(1);
        } else
        {
            movement.Stop();
        }

        if (movement.CheckGrounded())
        {
            stateController.ChangeState<IdleState>();
        }

    }

    public void Exit()
    {

    }
}
