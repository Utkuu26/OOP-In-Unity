using UnityEngine;

public enum PickupType { WeaponPistol, WeaponShotgun, WeaponRifle, HealthSmall, HealthFull, Speed, Ammo }

public class PickupItem : MonoBehaviour
{
    public PickupType pickupType;
    public ParticleSystem pickupVFX;

    private void Awake()
    {
        pickupVFX = GetComponentInChildren<ParticleSystem>(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        PlayerPickupHandler pickupHandler = other.GetComponent<PlayerPickupHandler>();
        WeaponManager weaponManager = other.GetComponent<WeaponManager>();
        AudioSource playerAudio = other.GetComponent<AudioSource>();

        bool shouldDestroy = true;

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

            case PickupType.Ammo:
                if (weaponManager != null)
                {
                    bool added = weaponManager.TryAddAmmo(10);
                    if (!added)
                    {
                        PlayerUIMessage uiMessage = other.GetComponent<PlayerUIMessage>();
                        if (uiMessage != null)
                            uiMessage.ShowMessage("Maximum ammunition capacity for the gun.");
                        shouldDestroy = false; // pickup alınmayacak
                    }
                }
                break;
        }

        if (shouldDestroy)
        {
            AudioClip clip = PickupSoundManager.Instance?.GetClip(pickupType);
            if (playerAudio != null && clip != null)
                playerAudio.PlayOneShot(clip);

            if (pickupVFX != null)
            {
                pickupVFX.transform.parent = null;
                pickupVFX.Play();
                Destroy(pickupVFX.gameObject, pickupVFX.main.duration);
            }

            Destroy(gameObject);
        }
    }
}
