using UnityEngine;
using System.Collections.Generic;

namespace EspartanosGameDev.Lessons.Lesson_Prefab
{
[CreateAssetMenu(fileName = "CardSO", menuName = "Card/CardSO")]
public class CardSO : ScriptableObject
{
    [SerializeField] string cardName;
    [SerializeField] Sprite cardImage;
    [SerializeField, TextArea(3, 10)] string cardDescription;

    [SerializeField] List<CardBehaviorSO> cardBehaviorsSO;

    public IEnumerable<CardBehaviorSO> CardBehaviors => cardBehaviorsSO;
}
}