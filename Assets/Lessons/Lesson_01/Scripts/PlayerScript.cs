using UnityEngine;

namespace EspartanosGameDev.Lessons.Lesson01
{
public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    PlayerInput inputs;
    Rigidbody2D rb;

    void Start()
    {
        // Need to use the new input system.
        inputs = new PlayerInput();
        inputs.Enable();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //OldInputSystem();
        //OldInputSystemWithRigidbody2D_V1();
        //OldInputSystemWithRigidbody2D_V2();
        //OldInputSystemWithRigidbody2D_V3(); // Probally I will not teach this one, but I will leave it here for reference.
        //NewInputSystemBasic();
        //NewInputSystemBasicWithRigidbody2D(); // Probally I will not teach this one, but I will leave it here for reference.
        NewInputSystemWithRigidbody2D();
    }

    void NewInputSystemBasic()
    {
        if (inputs.Player.Left.IsPressed())
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (inputs.Player.Right.IsPressed())
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (inputs.Player.Up.IsPressed())
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (inputs.Player.Down.IsPressed())
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    void OldInputSystem()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    void OldInputSystemWithRigidbody2D_V1()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //rb.linearVelocity = Vector2.left * speed;
            rb.linearVelocityX = -speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //rb.linearVelocity = Vector2.right * speed;
            rb.linearVelocityX = speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            //rb.linearVelocity = Vector2.up * speed;
            rb.linearVelocityY = speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //rb.linearVelocity = Vector2.down * speed;
            rb.linearVelocityY = -speed;
        }
    }

    void OldInputSystemWithRigidbody2D_V2()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.linearVelocityX = -speed;
        } else if (Input.GetKey(KeyCode.D))
        {
            rb.linearVelocityX = speed;
        } else
        {
            rb.linearVelocityX = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.linearVelocityY = speed;
        } else if (Input.GetKey(KeyCode.S))
        {
            rb.linearVelocityY = -speed;
        } else
        {
            rb.linearVelocityY = 0;
        }
    }

    void OldInputSystemWithRigidbody2D_V3()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1f;
        } else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1f;
        } else if (Input.GetKey(KeyCode.S))
        {
            vertical = -1f;
        }

        // Move diagonally more faster if use this method
        //rb.linearVelocityX = horizontal * speed;
        //rb.linearVelocityY = vertical * speed;

        // Normalize the vector to move diagonally at the same speed as moving in a straight line
        rb.linearVelocity = new Vector2(horizontal, vertical).normalized * speed;
    }

    void NewInputSystemBasicWithRigidbody2D()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (inputs.Player.Left.IsPressed())
        {
            horizontal = -1f;
        } else if (inputs.Player.Right.IsPressed())
        {
            horizontal = 1f;
        }

        if (inputs.Player.Up.IsPressed())
        {
            vertical = 1f;
        } else if (inputs.Player.Down.IsPressed())
        {
            vertical = -1f;
        }

        rb.linearVelocity = new Vector2(horizontal, vertical).normalized * speed;
    }

    void NewInputSystemWithRigidbody2D()
    {
        float horizontal = inputs.Player.MoveX.ReadValue<float>();
        float vertical = inputs.Player.MoveY.ReadValue<float>(); // Comment this line to create a plataform game

        rb.linearVelocity = new Vector2(horizontal, vertical).normalized * speed;
    }
}
}

