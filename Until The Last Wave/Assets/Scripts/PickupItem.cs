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
                        playerHealth.RestoreHealth(playerHealth.maxHealth / 2); // Can� %50 art�r
                        break;
                    case PickupType.HealthFull:
                        playerHealth.SetFullHealth(); // Tam can yap
                        break;
                    case PickupType.Speed:
                        // �u anl�k speed boost yapm�yoruz, sonra ekleriz
                        break;
                    case PickupType.Weapon:
                        // �u anl�k silah de�i�tirme yapm�yoruz, sonra ekleriz
                        break;
                }
            }

            Destroy(gameObject); // Pickup al�nca yok olsun
        }
    }
}
