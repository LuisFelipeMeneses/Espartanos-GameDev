using System;
using UnityEngine;

namespace EspartanosGameDev.Lessons.Base_Lesson
{
public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float deltaGroundDistance = 0.1f;
    [SerializeField] float minGroundNormalY = 0.7f;
    [SerializeField] float catchDistance;
    bool isGrounded = false;
    bool pressJump = false;
    bool pressAttack = false;
    bool isCrouching = false;
    bool isSprinting = false;
    bool canCallAttack = false;
    bool isSprintingAir = false;
    bool isPushPulling = false;
    float moveX;

    PlayerInputs actions;
    [SerializeField] ContactFilter2D contactFilter;
    [SerializeField] LayerMask catchableLayerMask;
    RaycastHit2D[] castHits = new RaycastHit2D[8];
    [SerializeField] GameObject hand;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    BoxCollider2D collider;
    Animator animator;
    CatchableObjScript caughtObject;
    PlayerState currentState = PlayerState.Grounded;

    public event Action<string> debugMessageEvent;

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
        debugMessageEvent?.Invoke($"isGrounded: {isGrounded}\nState: {currentState}\n Velocity: {rb.linearVelocityX}, {rb.linearVelocityY}");
        moveX = actions.Player.MoveX.ReadValue<float>();
        pressJump = actions.Player.Jump.IsPressed();
        pressAttack = actions.Player.Attack.IsPressed();
        isCrouching = actions.Player.Crouch.IsPressed();
        isSprinting = actions.Player.Sprint.IsPressed();
        isPushPulling = actions.Player.PushPull.IsPressed();
        UpdateAnimator();
        UpdateHitBox();

        switch (currentState)
        {
            case PlayerState.Grounded:
                GroundedState();
                break;
            case PlayerState.Airborne:
                AirborneState();
                break;
            case PlayerState.Attack:
                AttackState();
                break;
            case PlayerState.PushPull:
                PushPullState();
                break;

        }
    }

    void GetComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void UpdateAnimator()
    {
        animator.SetFloat("xSpeed", Mathf.Abs(moveX));
        animator.SetFloat("ySpeed", rb.linearVelocityY);
        //animator.SetBool("IsJumping", !isGrounded);
        animator.SetBool("IsCrouching", isCrouching);
        if (moveX != 0)
        {
            animator.SetBool("IsSprinting", isSprinting);           
        } else
        {
            animator.SetBool("IsSprinting", false);
        }
    }

    void UpdateHitBox()
    {
        if (isCrouching)
        {
            collider.offset = new Vector2(0, -0.6935129f);
            collider.size = new Vector2(0.79f, 1.279624f);
        }
        else
        {
            collider.offset = new Vector2(0, -0.3882946f);
            collider.size = new Vector2(0.79f, 1.890061f);
        }
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

    #region States

    void GroundedState()
    {
        MoveAndFlip();
        if (pressJump)
        {
            Jump();
            return;
        }
        if (pressAttack)
        {
            ChangeState(PlayerState.Attack);
            return;
        }
        if (isPushPulling)
        {
            ChangeState(PlayerState.PushPull);
            return;
        }
    }

    void AirborneState()
    {
        if (rb.linearVelocityY < 1)
        {
            isGrounded = CheckGrounded();
        }
        Move();
        FlipSprite();
        if (isGrounded)
        {
            ChangeState(PlayerState.Grounded);
        }
    }

    void AttackState()
    {
        rb.linearVelocityX = 0;
        FlipSprite();
        if (!pressAttack && canCallAttack)
        {
            ChangeState(PlayerState.Grounded);
            return;
        }
        if (pressJump && canCallAttack)
        {
            Jump();
        }
    }

    void PushPullState()
    {
        if (!isPushPulling)
        {
            ChangeState(PlayerState.Grounded);
            return;
        }
        MoveAndFlip();
        float dirPushPull;

        if (spriteRenderer.flipX == true)
        {
            dirPushPull = moveX * -1;
        }
        else
        {
            dirPushPull = moveX;
        }
        animator.SetFloat("dirPushPull", dirPushPull);
    }

    void Jump()
    {
        rb.linearVelocityY = jumpForce;
        ChangeState(PlayerState.Airborne);
    }

    void MoveAndFlip()
    {
        Move();
        FlipSprite();
        CheckFall();
    }

    void Move()
    {
        float targetSpeed;

        if ((isCrouching && isGrounded) || currentState == PlayerState.PushPull)
        {
            targetSpeed = speed * 0.3f * moveX;
        }
        else if (isSprinting && isGrounded)
        {
            targetSpeed = speed * 1.5f * moveX;
            isSprintingAir = true;
        }
        else
        {
            if (isSprintingAir && currentState == PlayerState.Airborne)
            {
                targetSpeed = speed * 1.5f * moveX;
            }
            else
            {
                targetSpeed = speed * moveX;
                isSprintingAir = false;
            }
        }
        if (Math.Sign(rb.linearVelocityX) != Math.Sign(targetSpeed))
        {
            rb.linearVelocityX = targetSpeed;
        }

        float diff = Math.Abs(rb.linearVelocityX - targetSpeed);
        if(diff < 0.1f)
        {
            rb.linearVelocityX = targetSpeed;
        } else
        {
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, targetSpeed, 0.1f);
        }
    }

    void FlipSprite()
    {
        if (currentState != PlayerState.PushPull)
        {
            if (moveX > 0)
            {
                hand.transform.localPosition = new Vector3(Math.Abs(hand.transform.localPosition.x), hand.transform.localPosition.y, hand.transform.localPosition.z);
                spriteRenderer.flipX = false;
            }
            else if (moveX < 0)
            {
                hand.transform.localPosition = new Vector3(-Math.Abs(hand.transform.localPosition.x), hand.transform.localPosition.y, hand.transform.localPosition.z);
                spriteRenderer.flipX = true;
            }
        }
    }

    void CheckFall()
    {
        if (rb.linearVelocityY < -1f)
        {
            ChangeState(PlayerState.Airborne);
        }
    }

    void ChangeState(PlayerState newState)
    {
        ExitState(currentState);
        currentState = newState;
        EnterState(newState);
    }

    void ExitState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Grounded:
                break;
            case PlayerState.Airborne:
                isSprintingAir = false;
                animator.SetBool("IsJumping", false);
                break;
            case PlayerState.Attack:
                canCallAttack = false;
                animator.SetBool("IsAttacking", false);
                break;
            case PlayerState.PushPull:
                caughtObject?.Release();
                caughtObject = null;
                animator.SetBool("IsPushPulling", false);
                break;
        }
    }

    void EnterState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Grounded:
                break;
            case PlayerState.Airborne:
                isGrounded = false;
                animator.SetBool("IsJumping", true);
                break;
            case PlayerState.Attack:
                animator.SetBool("IsAttacking", true);
                break;
            case PlayerState.PushPull:
                if (!TryCatch())
                {
                    ChangeState(PlayerState.Grounded);

                } else
                {
                    animator.SetBool("IsPushPulling", true);
                }
                break;
        }
    }

    #endregion

    void CancelAttack()
    {
        canCallAttack = true;
    }

    bool TryCatch()
    {
        Vector2 direction = spriteRenderer.flipX
            ? Vector2.left
            : Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction,
            catchDistance,
            catchableLayerMask
        );
        Vector2 debugDirection = new Vector2(direction.x * catchDistance, direction.y * catchDistance);

        Debug.DrawRay(transform.position, debugDirection, Color.red, 1f);

        if (hit.collider != null)
        {
            CatchableObjScript catchable = hit.collider.GetComponent<CatchableObjScript>();
            if (catchable != null)
            {
                if (catchable.Catch(hand.transform))
                {
                    caughtObject = catchable;
                }
                return true;
            }
        }
        return false;
    }

    enum PlayerState
    {
        Grounded,
        Airborne,
        Attack,
        PushPull
    }
}
}