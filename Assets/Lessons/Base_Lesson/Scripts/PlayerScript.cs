using UnityEngine;

namespace EspartanosGameDev.Lessons.Base_Lesson
{
public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float deltaGroundDistance = 0.1f;
    [SerializeField] float minGroundNormalY = 0.7f;
    bool isGrounded = false;
    float moveX;

    PlayerInputs actions;
    [SerializeField] ContactFilter2D contactFilter;
    RaycastHit2D[] castHits = new RaycastHit2D[8];

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Collider2D collider;
    Animator animator;

    void Awake()
    {
        GetComponents();
        actions = new PlayerInputs();
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
        
    }

    void Update()
    {
        isGrounded = CheckGrounded();
        UpdateAnimator();
        moveX = actions.Player.MoveX.ReadValue<float>();
        rb.linearVelocityX = moveX * speed;
        if (rb.linearVelocityX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.linearVelocityX < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (actions.Player.Jump.triggered && isGrounded)
        {
            rb.linearVelocityY = jumpForce;
        }
    }

    void GetComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void UpdateAnimator()
    {
        animator.SetFloat("xSpeed", Mathf.Abs(moveX));
        animator.SetFloat("ySpeed", rb.linearVelocityY);
        animator.SetBool("IsJumping", !isGrounded);
    }

    bool CheckGrounded()
    {
        int hitCount = collider.Cast(Vector2.down, contactFilter, castHits, deltaGroundDistance);
        for (int i = 0; i < hitCount; i++)
        {
            if (castHits[i].normal.y >= minGroundNormalY)
            {
                return true;
            }
        }
        return false;
    }
}
}