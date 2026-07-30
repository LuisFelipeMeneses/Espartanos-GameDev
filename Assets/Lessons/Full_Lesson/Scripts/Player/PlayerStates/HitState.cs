using UnityEngine;
public class HitState : IPlayerState
{
    PlayerStateController stateController;
    PlayerMovement playerMovement;
    PlayerMovementSettings movementSettings;

    float duration;

    public HitState(PlayerMovement playerMovement, PlayerStateController stateController, PlayerMovementSettings movementSettings)
    {
        this.playerMovement = playerMovement;
        this.stateController = stateController;
        this.movementSettings = movementSettings;
    }

    public void Enter()
    {
        duration = movementSettings.knockbackDuration;
    }

    public void Exit()
    {
        playerMovement.Stop();
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
}
