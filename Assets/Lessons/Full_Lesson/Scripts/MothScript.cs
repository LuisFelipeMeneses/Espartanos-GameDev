using UnityEngine;

public class MothScript : MonoBehaviour
{
    [SerializeField] float knockbackForce = 5f;
    [SerializeField] float maxY;
    [SerializeField] float minY;
    [SerializeField] float velocity;
    bool isUping;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerScript>(
            out PlayerScript player))
        {
            
            Vector2 direction = new Vector2(
                player.transform.position.x - transform.position.x,
                0).normalized;

            player.TakeDamage(1, direction * knockbackForce);
        }
    }

    void FixedUpdate()
    {
        if (transform.position.y < maxY && isUping)
        {
            rb.linearVelocityY = velocity;
        }

        if (transform.position.y > minY && !isUping)
        {
            rb.linearVelocityY = -velocity;
        }

        if (transform.position.y  >= maxY)
        {
            isUping = false;
        }

        if (transform.position.y <= minY)
        {
            isUping = true;
        }
    }
}
