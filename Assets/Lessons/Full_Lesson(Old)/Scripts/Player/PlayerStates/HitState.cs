using UnityEngine;

namespace EspartanosGameDev.Lessons.FullLessonOld
{
public class HitState : IPlayerState
{
    PlayerStateController stateController;
    PlayerMovement playerMovement;
    PlayerMovementSettings movementSettings;

    float duration;
    Vector2 knockback;
    SpriteRenderer spriteRenderer;

    public HitState(PlayerStateController stateController, PlayerMovement playerMovement, PlayerMovementSettings movementSettings, SpriteRenderer spriteRenderer)
    {
        this.stateController = stateController;
        this.playerMovement = playerMovement;
        this.movementSettings = movementSettings;
        this.spriteRenderer = spriteRenderer;
    }

    public void Enter()
    {
        duration = movementSettings.knockbackDuration;
        playerMovement.Knockback(knockback);
        spriteRenderer.material.SetFloat("_FlashAmount", 1f);
    }

    public void Exit()
    {
        playerMovement.Stop();
        spriteRenderer.material.SetFloat("_FlashAmount", 0f);
    }

    public void FixedUpdate()
    {
        
    }

    public void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            stateController.ChangeState<IdleState>();
        }
    }

    public void SetKnockback(Vector2 knockback)
    {
        this.knockback = knockback;
    }
}
}