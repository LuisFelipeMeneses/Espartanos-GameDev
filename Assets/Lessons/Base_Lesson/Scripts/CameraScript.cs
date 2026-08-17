using UnityEngine;

namespace EspartanosGameDev.Lessons.Base_Lesson
{
public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float minY = 0;
    [SerializeField] float minX = 0;
    void Start()
    {
        
    }

    void Update()
    {
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;
        targetPosition.x = Mathf.Max(targetPosition.x, minX);
        targetPosition.y = Mathf.Max(targetPosition.y, minY);
        transform.position = targetPosition;
    }
}
}