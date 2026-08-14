using UnityEngine;

namespace EspartanosGameDev.Lessons.Lesson_Prefab
{
[CreateAssetMenu(fileName = "CardDamage", menuName = "Card/Behaviors/CardDamage")]
public class CardDamageBehaviorSO : CardBehaviorSO
{
    [SerializeField] float damageAmount;
    
    public override void ExecuteBehavior(CardScript card)
    {
        card.SetDamage(damageAmount);
    }
}
}