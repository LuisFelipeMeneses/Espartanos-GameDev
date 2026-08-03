using UnityEngine;

namespace EspartanosGameDev.Lessons.Lesson01
{
public class PlayerScript : MonoBehaviour
{

    #region New Input System(More complex)
    /*
    PlayerInput inputs;
    public float speed = 5f;

    void Awake()
    {
        inputs = new PlayerInput();
    }
    void Start()
    {
        
    }

    void OnEnable()
    {
        inputs.Enable();
    }

    void OnDisable()
    {
        inputs.Disable();
    }

    // Update is called once per frame
    void Update()
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
    */
    #endregion

    #region Old Input System (Simpler)
    public float speed = 5f;

    void Start()
    {
        
    }

    void Update()
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

    #endregion

}
}

