using UnityEngine;

namespace EspartanosGameDev.Lessons.FullLessonOld
{
public class IdleState : IPlayerState
{
    private readonly PlayerActions actions;
    private readonly PlayerStateController stateController;

    public IdleState(PlayerStateController stateController,PlayerActions actions)
    {
        this.stateController = stateController;
        this.actions = actions;
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
}