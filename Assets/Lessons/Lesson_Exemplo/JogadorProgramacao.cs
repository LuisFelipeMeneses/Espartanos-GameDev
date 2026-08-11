using UnityEngine;

public class JogadorProgramacao : MonoBehaviour
{
    
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        transform.position += new Vector3(1f * Time.deltaTime, 0, 0);
    }
}
