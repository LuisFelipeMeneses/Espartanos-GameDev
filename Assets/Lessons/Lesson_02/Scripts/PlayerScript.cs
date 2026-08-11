using UnityEngine;

namespace EspartanosGameDev.Lessons.Lesson02
{
public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    bool isGrounded = false;
    PlayerInput inputs;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Collider2D collider;
    public LayerMask groundLayer;
    public float rayDistance = 1.4f;
    RaycastHit2D[] castHits = new RaycastHit2D[8];
    public ContactFilter2D contactFilter;

    void Start()
    {
        inputs = new PlayerInput();
        inputs.Enable();

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float horizontal = inputs.Player.MoveX.ReadValue<float>();
        bool jumpPressed = inputs.Player.Jump.WasPressedThisFrame();

        rb.linearVelocityX = horizontal * speed;
        //IsGroundedV1();
        //IsGroundedV2();
        //IsGroundedV3();
        //IsGroundedV4();
        //IsGroundedNormalCast();

        if (jumpPressed && isGrounded)
        {
            rb.linearVelocityY = jumpForce;
        }

        if (horizontal < 0)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0) // Use else if to avoid flipping the sprite when horizontal is 0
        {
            //transform.localScale = new Vector3(1, 1, 1);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            spriteRenderer.flipX = false;
        }
    }

    void IsGroundedV1()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);
        isGrounded = hit.collider != null;

        Debug.DrawRay(transform.position, Vector2.down * 0.2f, Color.red);
        Debug.Log("Hit: " + hit.collider);
    }

    void IsGroundedV2()
    {
        Vector2 position = new Vector2(collider.bounds.center.x, collider.bounds.min.y - 0.01f);
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 0.1f);
        isGrounded = hit.collider != null;
        
        Debug.DrawRay(position, Vector2.down * 0.1f, Color.red);
        Debug.Log("Hit: " + hit.collider);
    }

    void IsGroundedV3()
    {
        Vector2 position = new Vector2(collider.bounds.center.x, collider.bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 0.1f, groundLayer);
        isGrounded = hit.collider != null;
        
        Debug.DrawRay(position, Vector2.down * 0.1f, Color.red);
        Debug.Log("Hit: " + hit.collider);
    }

    void IsGroundedV4()
    {
        // Change the distance of the raycast to a variable that can be modified in the inspector
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, /*1.4f*/ rayDistance, groundLayer);
        isGrounded = hit.collider != null;

        Debug.DrawRay(transform.position, Vector2.down * /*1.4f*/ rayDistance, Color.red);
        Debug.Log("Hit: " + hit.collider);
    }

    void IsGroundedNormalCast()
    {
        int hits = collider.Cast(Vector2.down, contactFilter, castHits, 0.1f);

        for (int i = 0; i < hits; i++)
        {
            if (castHits[i].normal.y >= 0.9f)
            {
                isGrounded = true;
                return;
            }
        }
    }
}
}
