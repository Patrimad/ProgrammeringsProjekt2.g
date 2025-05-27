using UnityEngine;

public class BloodOrbs : MonoBehaviour
{
    public int amountHeal;
    public float lifeDuration = 5f;
    public bool isHeal;

    private void Start()
    {
        Destroy(gameObject, lifeDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health_Stam playerHealth = other.GetComponent<Health_Stam>();
            if (playerHealth != null)
            {
                if (isHeal == true)
                {
                    if (amountHeal + playerHealth.currentHealth >= playerHealth.maxHealth)
                    {
                        playerHealth.currentHealth = playerHealth.maxHealth;
                    }
                    else
                    {
                        playerHealth.GiveHealth(amountHeal);
                    }
                }
                else
                {
                    if (amountHeal - playerHealth.currentHealth >= playerHealth.maxHealth)
                    {
                        playerHealth.currentHealth = playerHealth.maxHealth;
                    }
                    else
                    {
                        playerHealth.GiveBlood(amountHeal);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}