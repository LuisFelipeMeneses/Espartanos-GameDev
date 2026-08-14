using UnityEngine;

namespace EspartanosGameDev.Lessons.Lesson_Prefab
{
[CreateAssetMenu(fileName = "EnemySettings", menuName = "Enemy/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    public float speed;
    public Sprite sprite;
}
}