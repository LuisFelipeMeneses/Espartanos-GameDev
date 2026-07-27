using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;

public class HealthScript : MonoBehaviour
{
    [SerializeField] PlayerScript player;
    [SerializeField] GameObject heartPrefab;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite halfHeart;
    [SerializeField] Sprite emptyHeart;

    List<Image> heartImages = new();

    void OnEnable()
    {
        player.OnHealthChanged += UpdateHealth;
    }

    void OnDisable()
    {
        player.OnHealthChanged -= UpdateHealth;
    }

    void UpdateHealth(int currentHealth, int maxHealth)
    {
        UpdateHeartsAmount(maxHealth);
        UpdateHeartsSprites(currentHealth);
    }

    void UpdateHeartsAmount(int maxHealth)
    {
        int heartsAmount = Mathf.CeilToInt(maxHealth / 2f);
        Debug.Log("Quantidade de corações: " + heartsAmount);
        while(heartImages.Count < heartsAmount)
        {
            GameObject newHeart = Instantiate(
            heartPrefab,
            transform
            );

            Image image = newHeart.GetComponent<Image>();

            heartImages.Add(image);
        }

        while(heartImages.Count > heartsAmount)
        {
            Image image = heartImages[^1];
            heartImages.RemoveAt(heartImages.Count - 1);
            Destroy(image.gameObject);
        }
    }

    void UpdateHeartsSprites(int currentHealth)
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            int healthForThisHeart = currentHealth - i * 2;

            if (healthForThisHeart >= 2)
            {
                heartImages[i].sprite = fullHeart;
            }
            else if (healthForThisHeart == 1)
            {
                heartImages[i].sprite = halfHeart;
            }
            else
            {
                heartImages[i].sprite = emptyHeart;
            }
        }
    }
}
