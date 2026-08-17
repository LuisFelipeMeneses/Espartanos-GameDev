using TMPro;
using UnityEngine;

namespace EspartanosGameDev.Lessons.Base_Lesson
{
public class DebugUIScript : MonoBehaviour
{
    TMP_Text debugText;
    PlayerScript playerScript;
    void Start()
    {
        debugText = GetComponentInChildren<TMP_Text>();
        playerScript = FindAnyObjectByType<PlayerScript>();
        playerScript.debugMessageEvent += UpdateDebugText;
    }

    void Update()
    {
        
    }

    void UpdateDebugText(string message)
    {
        debugText.text = message;
    }
}
}