using System;
using UnityEngine;

namespace EspartanosGameDev.Lessons.FullLessonOld
{
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
        GetComponents();

        actions = new PlayerActions();
        playerMovement = new PlayerMovement(rb, collider, spriteRenderer, movementSettings);

        stateController = new PlayerStateController(actions, playerMovement, animator, movementSettings, spriteRenderer);
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
        UpdateLife();
        stateController.ChangeState<IdleState>();
    }

    void Update()
    {
        stateController.Update();
    }

    void FixedUpdate()
    {
        stateController.FixedUpdate();
        UpdateAnimator();
        
    }

    void GetComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void UpdateAnimator()
    {
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

    void UpdateLife()
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    
}
}