using UnityEngine;

namespace EspartanosGameDev.Lessons.Lesson_Prefab
{
public class CardScript : MonoBehaviour
{
    [SerializeField] CardSO cardSO;
    float damage;
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Use()
    {
        foreach (var behavior in cardSO.CardBehaviors)
        {
            behavior.ExecuteBehavior(this);
        }
    }

    public void SetDamage(float damageAmount)
    {
        damage = damageAmount;
    }
}
}