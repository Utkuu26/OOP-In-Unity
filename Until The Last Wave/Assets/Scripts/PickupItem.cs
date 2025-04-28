using UnityEngine;

public enum PickupType { WeaponPistol, WeaponShotgun, WeaponRifle, HealthSmall, HealthFull, Speed }

public class PickupItem : MonoBehaviour
{
    public PickupType pickupType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            PlayerPickupHandler pickupHandler = other.GetComponent<PlayerPickupHandler>();
            WeaponManager weaponManager = other.GetComponent<WeaponManager>();

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
            else if (weaponManager != null)
            {
                if (pickupType == PickupType.WeaponPistol)
                    weaponManager.EquipWeapon(WeaponType.Pistol);
                else if (pickupType == PickupType.WeaponShotgun)
                    weaponManager.EquipWeapon(WeaponType.Shotgun);
                else if (pickupType == PickupType.WeaponRifle)
                    weaponManager.EquipWeapon(WeaponType.Rifle);
            }

            Destroy(gameObject); 
        }
    }
}
