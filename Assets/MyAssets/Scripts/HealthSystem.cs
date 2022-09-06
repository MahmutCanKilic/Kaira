using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    private float currentHealth;
    private float maxHealth;
    [SerializeField] private Image healthBar;
    private void Awake()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    private void Update()
    {

    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth / maxHealth;

    }
}
