using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Player took {amount} damage. Remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            
            Die();
        }
    }

    void Die()
    {
        var manager = FindFirstObjectByType<GameManager>();
        manager?.PlayerDied();
       
    }
}
