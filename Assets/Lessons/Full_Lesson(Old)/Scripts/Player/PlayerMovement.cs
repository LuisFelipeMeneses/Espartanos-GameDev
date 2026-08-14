using UnityEngine;

namespace EspartanosGameDev.Lessons.FullLessonOld
{
public class PlayerMovement
{
    readonly Rigidbody2D rb;
    readonly Collider2D collider;
    readonly SpriteRenderer spriteRenderer;

    public float linearVelocityX
    {
        get => rb.linearVelocity.x;
    }

    public float linearVelocityY
    {
        get => rb.linearVelocity.y;
    }

    readonly PlayerMovementSettings settings;

    RaycastHit2D[] castHits = new RaycastHit2D[8];

    public PlayerMovement(Rigidbody2D rb, Collider2D collider, SpriteRenderer spriteRenderer, PlayerMovementSettings settings)
    {
        this.rb = rb;
        this.collider = collider;
        this.spriteRenderer = spriteRenderer;
        this.settings = settings;
    }

    public void MoveX(float input)
    {
        rb.linearVelocityX = input * settings.xSpeed;

        if (input < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    public void Jump()
    {
        rb.linearVelocityY = settings.jumpForce;
    }

    public void Stop()
    {
        rb.linearVelocityX = 0;
    }

    public void Knockback(Vector2 force)
    {
        rb.linearVelocity = force;
    }

    public bool CheckGrounded()
    {
        int hitCount = collider.Cast(Vector2.down, settings.contactFilter, castHits, settings.checkDistance);
        for (int i = 0; i < hitCount; i++)
        {
            if (castHits[i].normal.y >= settings.minGroundNormalY)
            {
                return true;
            }
        }
        return false;
    }
}
}