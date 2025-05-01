using UnityEngine;

public enum PickupType { WeaponPistol, WeaponShotgun, WeaponRifle, HealthSmall, HealthFull, Speed }

public class PickupItem : MonoBehaviour
{
    public PickupType pickupType;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        PlayerPickupHandler pickupHandler = other.GetComponent<PlayerPickupHandler>();
        WeaponManager weaponManager = other.GetComponent<WeaponManager>();
        AudioSource playerAudio = other.GetComponent<AudioSource>();

        // Pickup etkisi
        switch (pickupType)
        {
            case PickupType.HealthSmall:
                playerHealth?.RestoreHealth(playerHealth.maxHealth / 2);
                break;
            case PickupType.HealthFull:
                playerHealth?.SetFullHealth();
                break;
            case PickupType.Speed:
                pickupHandler?.ActivateSlowMotion();
                break;
            case PickupType.WeaponPistol:
                weaponManager?.EquipWeapon(WeaponType.Pistol);
                break;
            case PickupType.WeaponShotgun:
                weaponManager?.EquipWeapon(WeaponType.Shotgun);
                break;
            case PickupType.WeaponRifle:
                weaponManager?.EquipWeapon(WeaponType.Rifle);
                break;
        }

        // Pickup sesini çal (Player'ýn AudioSource'undan)
        AudioClip clip = PickupSoundManager.Instance?.GetClip(pickupType);
        if (playerAudio != null && clip != null)
            playerAudio.PlayOneShot(clip);

        Destroy(gameObject);
    }
}
