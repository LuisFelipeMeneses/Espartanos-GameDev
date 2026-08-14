using UnityEngine;
using UnityEngine.InputSystem;

namespace EspartanosGameDev.Lessons.Lesson03
{
public class PlayerScript : MonoBehaviour
{

    Rigidbody2D rb;
    PlayerInputs inputs;
    void Start()
    {
        inputs = new PlayerInputs();
        inputs.Enable();

        
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        /*
        Input.GetKeyDown(KeyCode.Space);
        Input.GetKey(KeyCode.W);
        Input.GetKeyUp(KeyCode.S);
        inputs.Player.Jump.WasPressedThisFrame();
        inputs.Player.Jump.IsPressed();
        inputs.Player.Jump.WasReleasedThisFrame();

        inputs.Player.MoveX.ReadValue<float>();

        bool spaceKey = Keyboard.current.spaceKey.wasPressedThisFrame;
        bool wKey = Keyboard.current.wKey.isPressed;
        bool sKey = Keyboard.current.sKey.wasReleasedThisFrame;
        bool bButton = Gamepad.current.bButton.wasPressedThisFrame;
        bool leftClick = Mouse.current.leftButton.wasPressedThisFrame;

        transform.position += Vector3.right * Time.deltaTime;
        */
        /*
        rb.linearVelocity = new Vector2(1,0);
        rb.linearVelocityX = 1;
        rb.linearVelocityY = 1;
        */
    }
}
}