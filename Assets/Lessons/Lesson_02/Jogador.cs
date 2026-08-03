using UnityEngine;

public class Jogador : MonoBehaviour
{
    public float velocidade = 0.01f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(velocidade, 0);
        
    }
}
