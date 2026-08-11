using UnityEngine;

namespace EspartanosGameDev.Lessons.Lesson_Prefab
{
public class PlayerScript : MonoBehaviour
{
    PlayerInputs inputs;
    float direction;
    public GameObject bulletPrefab;
    Rigidbody2D rb;
    SpriteRenderer sr;
    void Start()
    {
        inputs = new PlayerInputs();
        inputs.Enable();

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = inputs.Player.MoveX.ReadValue<float>();
        if (horizontal != 0)
        {
            direction = horizontal;
            sr.flipX = horizontal < 0;
        }
        if (inputs.Player.Shoot.WasPressedThisFrame())
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletScript>().Initialize(new Vector2(direction * 6, 0));

        }
        rb.linearVelocityX = horizontal * 5;

        if (inputs.Player.Jump.WasPressedThisFrame())
        {
            rb.linearVelocityY = 5;
        }

    }
}
}