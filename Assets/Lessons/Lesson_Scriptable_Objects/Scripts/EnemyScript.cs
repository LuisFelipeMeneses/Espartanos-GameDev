using UnityEngine;

namespace EspartanosGameDev.Lessons.Lesson_Prefab
{
public class EnemyScript : MonoBehaviour
{
    [SerializeField] private EnemySettings enemySettings;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        rb.linearVelocityX = enemySettings.speed;
        spriteRenderer.sprite = enemySettings.sprite;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}