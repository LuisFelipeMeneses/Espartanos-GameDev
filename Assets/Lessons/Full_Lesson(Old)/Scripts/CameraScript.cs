using UnityEngine;

namespace EspartanosGameDev.Lessons.FullLessonOld
{
public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float minX;
    [SerializeField] float minY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float tx = target.position.x;
        float ty = target.position.y;

        pos.x = tx > minX ? tx : minX;
        pos.y = ty > minY ? ty : minY;
        
        transform.position = pos;
    }
}
}