using UnityEngine;

namespace EspartanosGameDev.Lessons.InputSystem
{
public class OldInputSystem : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        // Check if the space key is pressed down in this frame. Use Keycode. and select the key you want to check.
        Input.GetKeyDown(KeyCode.Space);

        // Check if the A key is pressed.
        Input.GetKey(KeyCode.A);

        // Check if the space key is released in this frame.
        Input.GetKeyUp(KeyCode.Space);

        // Get the value of the horizontal axis with smoothing. The value will be between -1 and 1, left arrow or A key will approache -1, right arrow or D key will approach 1, and no input will be 0. The value will change smoothly over time.
        Input.GetAxis("Horizontal");
        // Get the value of the vertical axis with smoothing. The value will be between -1 and 1, down arrow or S key will approache -1, up arrow or W key will approach 1, and no input will be 0. The value will change smoothly over time.
        Input.GetAxis("Vertical");
        
        // Get the value of the horizontal axis without smoothing.
        Input.GetAxisRaw("Horizontal");
        // Get the value of the vertical axis without smoothing.
        Input.GetAxisRaw("Vertical");

        //BasicMovement();
        AxisMovement();
    }

    void BasicMovement()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime; // Multiply by Time.deltaTime to make the movement framerate independent.
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    void AxisMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // Horizontal axis is the x and vertical axis is the y.
        transform.position += new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
    }
}
}