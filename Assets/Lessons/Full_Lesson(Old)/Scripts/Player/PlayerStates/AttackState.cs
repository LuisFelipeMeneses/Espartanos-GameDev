using UnityEngine;

namespace EspartanosGameDev.Lessons.FullLessonOld
{
public class AttackState : IPlayerState
{
    private readonly PlayerActions actions;
    readonly PlayerMovement movement;
    private readonly PlayerStateController stateController;
    readonly Animator animator;

    bool canCancelAttack = false;

    public AttackState(PlayerStateController stateController, PlayerActions actions, Animator animator, PlayerMovement movement)
    {
        this.stateController = stateController;
        this.actions = actions;
        this.animator = animator;
        this.movement = movement;
    }

    public void Enter()
    {
        animator.SetBool("isAttacking", true);
        canCancelAttack = false;
        movement.Stop();
    }

    public void Update()
    {
        //animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1
        if (canCancelAttack && (!actions.Keyboard.Attack.IsPressed() ||
            actions.Keyboard.Jump.IsPressed()))
        {
            stateController.ChangeState<IdleState>();
        }
    }

    public void FixedUpdate()
    {
        
    }

    public void Exit()
    {
        animator.SetBool("isAttacking", false);
        canCancelAttack = false;
    }

    public void CancelAttack()
    {
        canCancelAttack = true;
    }
}
}