using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerController playerController;
    public Slider healthSlider;
    public TextMeshProUGUI healthPercentageText;

    private void Start()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        if (healthSlider == null)
        {
            healthSlider = GetComponentInChildren<Slider>();
        }

        healthSlider.maxValue = playerController.maxHealth;
        healthSlider.value = playerController.health;
    }

    private void Update()
    {
        healthSlider.value = playerController.health;
        UpdateHealthPercentageText();
    }

    private void UpdateHealthPercentageText()
    {
        float healthPercentage = (playerController.health / playerController.maxHealth) * 100f;
        healthPercentageText.text = $"{healthPercentage}%";
    }
}
