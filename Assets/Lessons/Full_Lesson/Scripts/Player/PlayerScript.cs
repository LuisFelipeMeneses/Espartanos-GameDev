using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int maxHealth = 6;
    [SerializeField] private int currentHealth = 6;
    [SerializeField] private PlayerMovementSettings movementSettings;

    private Rigidbody2D rb;
    private Collider2D collider;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    PlayerActions actions;
    PlayerMovement playerMovement;
    PlayerStateController stateController;

    public event Action<int, int> OnHealthChanged;
    

    void Awake()
    {
        stateController = new PlayerStateController();
        actions = new PlayerActions();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = new PlayerMovement(rb, collider, spriteRenderer, movementSettings);
        stateController.Register(new IdleState(actions, stateController));
        stateController.Register(new RunState(playerMovement, stateController, actions));
        stateController.Register(new JumpState(playerMovement, stateController, actions));
        stateController.Register(new HitState(playerMovement, stateController, movementSettings));
        stateController.Register(new AttackState(actions, stateController, animator, playerMovement));
    }

    void OnEnable()
    {
        actions.Enable();
    }

    void OnDisable()
    {
        actions.Disable();
    }

    void Start()
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        stateController.ChangeState<IdleState>();
    }

    void Update()
    {
        stateController.Update();
    }

    void FixedUpdate()
    {
        stateController.FixedUpdate();
        animator.SetFloat("xSpeed", Math.Abs(playerMovement.linearVelocityX));
        animator.SetFloat("ySpeed", playerMovement.linearVelocityY);
        animator.SetBool("isJumping", !playerMovement.CheckGrounded());
        
    }

    public void TakeDamage(int damage, Vector2 knockback)
    {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        stateController.GetState<HitState>().SetKnockback(knockback);
        stateController.ChangeState<HitState>();
    }

    public void CancelAttack()
    {
        stateController.GetState<AttackState>().CancelAttack();
    }
    
}