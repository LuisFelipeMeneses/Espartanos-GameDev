using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] int maxLife;
    [SerializeField] int currentLife;

    bool isGrounded;
    bool isKnockback;
    float movement;
    bool jumpPressed;
    bool attackPressed;
    [SerializeField] Collider2D collider;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float checkDistance = 0.05f;
    [SerializeField] float minGroundNormalY = 0.65f;

    PlayerActions inputs;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    readonly RaycastHit2D[] raycastHits = new RaycastHit2D[8];
    private ContactFilter2D contactFilter;

    public event Action<int, int> OnHealthChanged;

    void Awake()
    {
        inputs = new PlayerActions();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        contactFilter.useTriggers = false;
        contactFilter.useLayerMask = true;
        contactFilter.SetLayerMask(groundLayer);

        
    }

    void Start()
    {
        OnHealthChanged?.Invoke(currentLife, maxLife);
    }

    void OnEnable()
    {
        inputs.Enable();
    }

    void OnDisable()
    {
        inputs.Disable();
    }

    void Update()
    {
        movement = inputs.Keyboard.Move.ReadValue<float>();
        jumpPressed = inputs.Keyboard.Jump.IsPressed();
        attackPressed = inputs.Keyboard.Attack.IsPressed();
    }

    void FixedUpdate()
    {
        isGrounded = CheckGrounded();
        if (!isKnockback)
        {
            rb.linearVelocityX = movement * speed;
        }

        if (attackPressed && isGrounded)
        {
            rb.linearVelocityX = 0;
        }

        if (jumpPressed && isGrounded)
        {
            rb.linearVelocityY = jumpForce;
        }

        if(movement > 0 && (!attackPressed || !isGrounded))
        {
            spriteRenderer.flipX = false;
        } else if (movement < 0 && (!attackPressed || !isGrounded))
        {
            spriteRenderer.flipX = true;
        }

        animator.SetFloat("xSpeed", Math.Abs(rb.linearVelocityX));
        animator.SetFloat("ySpeed", rb.linearVelocityY);
        animator.SetBool("isJumping", !isGrounded);
        animator.SetBool("isAttacking", attackPressed);
    }

    bool CheckGrounded()
    {
        int hitCount = collider.Cast(Vector2.down, contactFilter, raycastHits, checkDistance);
        for (int i = 0; i < hitCount; i++)
        {
            if (raycastHits[i].normal.y >= minGroundNormalY)
            {
                return true;
            }
        }
        return false;
    }

    public void TakeDamage(int damage)
    {
        currentLife-= damage;
        OnHealthChanged?.Invoke(currentLife, maxLife);
    }

    void Healing(int currentLifeHeal, int maxLifeHeal)
    {
        currentLife += currentLifeHeal;
        maxLife += maxLifeHeal;
        OnHealthChanged?.Invoke(currentLife, maxLife);
    }

    public void ApplyKnockback(Vector2 force)
    {
        isKnockback = true;
        rb.linearVelocity = force;

        Invoke(nameof(EndKnockback), 0.2f);
    }

    void EndKnockback()
    {
        isKnockback = false;
    }
}