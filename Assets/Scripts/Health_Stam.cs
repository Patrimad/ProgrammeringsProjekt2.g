using UnityEngine;
using TMPro;

public class Health_Stam : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int maxBlood = 100;
    public int currentBlood;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI bloodText;

    public BarScript healthBar;
    public BarScript bloodBar;

    void Start()
    {
        currentHealth = maxHealth;
        currentBlood = maxBlood;
        UpdateHealthUI();
        UpdateBloodUI();
        healthBar.SetMaxhealth(maxHealth);
        bloodBar.SetMaxhealth(maxBlood);
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHealthUI();
        healthBar.SetHealth(currentHealth);
    }

    public void GiveHealth(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        UpdateHealthUI();
        healthBar.SetHealth(currentHealth);
    }

    public void TakeBlood(int amount)
    {
        currentBlood = Mathf.Max(0, currentBlood - amount);
        UpdateBloodUI();
        bloodBar.SetHealth(currentBlood);
    }

    public void GiveBlood(int amount)
    {
        currentBlood = Mathf.Min(maxBlood, currentBlood + amount);
        UpdateBloodUI();
        bloodBar.SetHealth(currentBlood);
    }

    private void UpdateHealthUI()
    {
        healthText.text = $"{currentHealth}/{maxHealth}";
    }

    private void UpdateBloodUI()
    {
        bloodText.text = $"{currentBlood}/{maxBlood}";
    }
}
