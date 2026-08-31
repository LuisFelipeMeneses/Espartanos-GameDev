using UnityEngine;
using UnityEngine.InputSystem;

namespace EspartanosGameDev.Lessons.InputSystem
{
public class NewInputSystem : MonoBehaviour
{
    PlayerInputs actions;
    public float speed = 5f;

    void Start()
    {
        actions = new PlayerInputs();
        actions.Enable();
    }

    void Update()
    {

        // Checking keys directly
        // Like the old input system, but wihout axis, and the value is getted by atributes of the key, like wasPressedThisFrame, isPressed, wasReleasedThisFrame, etc.
        // Instead methods like GetKeyDown(), GetKey(), GetKeyUp(), etc.
        bool spacePressedThisFrame = Keyboard.current.spaceKey.wasPressedThisFrame;
        bool aPressed = Keyboard.current.aKey.isPressed;
        bool spaceReleased = Keyboard.current.spaceKey.wasReleasedThisFrame;

        // Using Actions

        // Check if the action was pressed this frame.
        actions.Player.Action.WasPressedThisFrame();
        // Check if the action is pressed this frame.
        actions.Player.Action.IsPressed();
        // Check if the action was released this frame.
        actions.Player.Action.WasReleasedThisFrame();
        // Get the value of the action(MoveX). The value will be between -1 and 1
        actions.Player.MoveX.ReadValue<float>();
        // Get the value of the action(MoveY). The value will be between -1 and 1
        actions.Player.MoveY.ReadValue<float>();

        //SimpleMovement();
        AxisMovement();

        // Is possible to use events with new input system
        actions.Player.Action.performed += ctx => Debug.Log("Action performed");
        actions.Player.Action.canceled += ctx => Debug.Log("Action canceled");
    }

    void SimpleMovement()
    {
        if(Keyboard.current.wKey.isPressed)
        {
            transform.position += Vector3.up * speed * Time.deltaTime; // Multiply by Time.deltaTime to make the movement framerate independent.
        }
        if(Keyboard.current.sKey.isPressed)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if(Keyboard.current.aKey.isPressed)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if(Keyboard.current.dKey.isPressed)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    void AxisMovement()
    {
        float horizontal = actions.Player.MoveX.ReadValue<float>();
        float vertical = actions.Player.MoveY.ReadValue<float>();
        // Horizontal axis is the x and vertical axis is the y.
        transform.position += new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
    }
}
}
