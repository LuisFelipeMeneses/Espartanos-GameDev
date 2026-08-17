using UnityEngine;

namespace EspartanosGameDev.Lessons.Base_Lesson
{
public class CatchableObjScript : MonoBehaviour
{
    Collider2D collider;
    void Awake()
    {
        collider = GetComponent<Collider2D>();
        Debug.Log($"Collider bounds: {collider.bounds}");
        Debug.Log($"Collider bounds min: {collider.bounds.min}");
        Debug.Log($"Collider bounds max: {collider.bounds.max}");
        Debug.Log($"Collider bounds size: {collider.bounds.size}");
        Debug.Log($"Collider bounds extents: {collider.bounds.extents}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Catch(Transform playerTransform)
    {
        transform.SetParent(playerTransform);

        float localX = transform.localPosition.x < 0 ? -collider.bounds.extents.x : collider.bounds.extents.x;

        transform.localPosition = new Vector3(
            localX,
            transform.localPosition.y,
            transform.localPosition.z
        );

        return true;
    }

    public bool Release()
    {
        transform.SetParent(null);
        return true;
    }
}
}