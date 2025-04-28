using UnityEngine;

public enum PickupType { Weapon, HealthSmall, HealthFull, Speed }

public class PickupItem : MonoBehaviour
{
    public PickupType pickupType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                switch (pickupType)
                {
                    case PickupType.HealthSmall:
                        playerHealth.RestoreHealth(playerHealth.maxHealth / 2); // Caný %50 artýr
                        break;
                    case PickupType.HealthFull:
                        playerHealth.SetFullHealth(); // Tam can yap
                        break;
                    case PickupType.Speed:
                        // Þu anlýk speed boost yapmýyoruz, sonra ekleriz
                        break;
                    case PickupType.Weapon:
                        // Þu anlýk silah deðiþtirme yapmýyoruz, sonra ekleriz
                        break;
                }
            }

            Destroy(gameObject); // Pickup alýnca yok olsun
        }
    }
}
