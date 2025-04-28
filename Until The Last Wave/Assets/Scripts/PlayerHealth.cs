using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void RestoreHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); 
        Debug.Log("Current Health: " + currentHealth);
    }

    public void SetFullHealth()
    {
        currentHealth = maxHealth;
        Debug.Log("Health Fully Restored");
    }
}
