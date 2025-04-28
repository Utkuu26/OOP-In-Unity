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
            PlayerPickupHandler pickupHandler = other.GetComponent<PlayerPickupHandler>();

            if (pickupType == PickupType.HealthSmall && playerHealth != null)
            {
                playerHealth.RestoreHealth(playerHealth.maxHealth / 2);
            }
            else if (pickupType == PickupType.HealthFull && playerHealth != null)
            {
                playerHealth.SetFullHealth();
            }
            else if (pickupType == PickupType.Speed && pickupHandler != null)
            {
                pickupHandler.ActivateSlowMotion();
            }

            Destroy(gameObject); 
        }
    }
}
