using UnityEngine;
using TMPro;

public class Health_Stam : MonoBehaviour, IDamage
{
    public int maxHealth = 100;
    public int currentHealth;

    public int maxBlood = 100;
    public int currentBlood;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI bloodText;

    public BarScript healthBar;
    public BarScript bloodBar;
    
    public GameObject OrbPrefab;

    void Start()
    {
        currentHealth = maxHealth;
        currentBlood = maxBlood;
        UpdateHealthUI();
        UpdateBloodUI();
        healthBar.SetMaxhealth(maxHealth);
        bloodBar.SetMaxhealth(maxBlood);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(OrbPrefab, gameObject.transform.position,  Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHealthUI();
        healthBar.SetHealth(currentHealth);

        //variable der henter spillerens healthkode og f�r enemy til at skade efter lidt delay
        

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
        if (healthText == null)
        {
            return;
        }
        healthText.text = $"{currentHealth}/{maxHealth}";
    }

    private void UpdateBloodUI()
    {
        bloodText.text = $"{currentBlood}/{maxBlood}";
    }
}
