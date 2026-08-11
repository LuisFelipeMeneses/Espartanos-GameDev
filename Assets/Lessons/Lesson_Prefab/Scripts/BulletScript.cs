using UnityEngine;

namespace EspartanosGameDev.Lessons.Lesson_Prefab
{
public class BulletScript : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 direction;
    float maxTime = 2f;
    float currentTime = 0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = direction;
    }

    
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector2 direction)
    {
        
        this.direction = direction;
    }
}
}