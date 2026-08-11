using UnityEngine;

namespace EspartanosGameDev.Lessons.Lesson_Animations
{
public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerInputs inputs;
    
    public float speed = 5f;
    public float jumpForce = 5f;
    Animator animator;
    SpriteRenderer spriteRenderer;

    Collider2D collider;
    public ContactFilter2D contactFilter;
    RaycastHit2D[] castHits = new RaycastHit2D[8];
    void Start()
    {
        inputs = new PlayerInputs();
        inputs.Enable();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        bool isGrounded = IsGrounded();
        animator.SetBool("Jumping", !isGrounded);
        if (inputs.Player.Jump.WasPressedThisFrame() && isGrounded)
        {
            rb.linearVelocityY = jumpForce;
        }

        

        rb.linearVelocityX = inputs.Player.MoveX.ReadValue<float>() * speed;
        animator.SetBool("Running", rb.linearVelocityX != 0);
        if(rb.linearVelocityX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(rb.linearVelocityX < 0)
        {
            spriteRenderer.flipX = true;
        }

    }

    bool IsGrounded()
    {
        int hits = collider.Cast(Vector2.down, contactFilter, castHits, 0.1f);
		
		for (int i = 0; i < hits; i++)
		{
			  if (castHits[i].normal.y >= 0.9f)
			  {
				    return true;
			  }
		}
        return false;
    }
}
}