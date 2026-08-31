using UnityEngine;
using UnityEngine.InputSystem;

namespace EspartanosGameDev.Lessons.SaveGame
{
public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocityX = Keyboard.current.aKey.isPressed ? -5f : Keyboard.current.dKey.isPressed ? 5f : 0f;
        rb.linearVelocityY = Keyboard.current.wKey.isPressed ? 5f : Keyboard.current.sKey.isPressed ? -5f : 0f;
    }
}
}