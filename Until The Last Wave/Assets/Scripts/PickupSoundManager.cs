using UnityEngine;

public class PickupSoundManager : MonoBehaviour
{
    public static PickupSoundManager Instance { get; private set; }

    public AudioClip pistolSound;
    public AudioClip shotgunSound;
    public AudioClip rifleSound;
    public AudioClip healthSmallSound;
    public AudioClip healthFullSound;
    public AudioClip speedSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public AudioClip GetClip(PickupType type)
    {
        return type switch
        {
            PickupType.WeaponPistol => pistolSound,
            PickupType.WeaponShotgun => shotgunSound,
            PickupType.WeaponRifle => rifleSound,
            PickupType.HealthSmall => healthSmallSound,
            PickupType.HealthFull => healthFullSound,
            PickupType.Speed => speedSound,
            _ => null
        };
    }
}
