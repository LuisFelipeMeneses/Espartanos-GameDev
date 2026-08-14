using UnityEngine;

namespace EspartanosGameDev.Lessons.Lesson_Prefab
{
public abstract class CardBehaviorSO : ScriptableObject
{
    public abstract void ExecuteBehavior(CardScript card);
}
}