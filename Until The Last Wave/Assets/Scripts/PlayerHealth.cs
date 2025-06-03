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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            var manager = FindFirstObjectByType<GameManager>();
            manager?.PlayerDied();
            Die();
        }
    }

    void Die()
    {

        // veya
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // yeniden baþlat
    }
}
