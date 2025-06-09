using UnityEngine;

public enum WeaponType { Pistol, Shotgun, Rifle }

[System.Serializable]
public class WeaponData
{
    public WeaponType type;
    public GameObject model;
    public Animator animator;
    public AudioClip fireSound;
    public ParticleSystem Flash;

    public float damage;
    public float fireRate;
    public int currentAmmo;
    public int maxAmmo;
}

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Settings")]
    public WeaponData[] weapons;

    [Header("References")]
    public Transform cameraTransform;
    public GameObject crosshairGameObject;

    [Header("ADS Settings")]
    public float adsLockDelay = 0.5f;

    private float adsTimer = 0f;
    private bool isAiming = false;
    private bool hasLockedFinalTransform = false;
    private Vector3 localOffsetPos;
    private Quaternion localOffsetRot;

    public WeaponType CurrentWeapon { get; private set; }

    private void Start()
    {
        if (weapons == null || weapons.Length == 0)
        {
            Debug.LogError("No weapons assigned to WeaponManager!");
            return;
        }

        EquipWeapon(WeaponType.Pistol);
    }

    private void Update()
    {
        if (isAiming && !hasLockedFinalTransform)
        {
            adsTimer += Time.deltaTime;
            if (adsTimer >= adsLockDelay)
            {
                Transform weaponTransform = GetCurrentWeaponTransform();
                if (weaponTransform != null && cameraTransform != null)
                {
                    localOffsetPos = Quaternion.Inverse(cameraTransform.rotation) * (weaponTransform.position - cameraTransform.position);
                    localOffsetRot = Quaternion.Inverse(cameraTransform.rotation) * weaponTransform.rotation;
                    hasLockedFinalTransform = true;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (isAiming && hasLockedFinalTransform)
        {
            Transform weaponTransform = GetCurrentWeaponTransform();
            if (weaponTransform != null)
            {
                weaponTransform.position = cameraTransform.position + cameraTransform.rotation * localOffsetPos;
                weaponTransform.rotation = cameraTransform.rotation * localOffsetRot;
            }
        }
    }

    public void EquipWeapon(WeaponType type)
    {
        CurrentWeapon = type;

        foreach (var weapon in weapons)
        {
            bool isActive = weapon.type == type;
            if (weapon.model != null)
                weapon.model.SetActive(isActive);
        }
    }

    public void ToggleAim()
    {
        isAiming = !isAiming;
        hasLockedFinalTransform = false;
        adsTimer = 0f;

        GetCurrentWeaponData()?.animator?.SetBool("IsAiming", isAiming);
        crosshairGameObject?.SetActive(!isAiming);
    }

    public int GetAmmo()
    {
        return GetCurrentWeaponData()?.currentAmmo ?? 0;
    }

    public void ConsumeAmmo()
    {
        var weapon = GetCurrentWeaponData();
        if (weapon != null && weapon.currentAmmo > 0)
        {
            weapon.currentAmmo--;
        }
    }

    public bool TryAddAmmo(int amount)
    {
        var weapon = GetCurrentWeaponData();
        if (weapon == null || weapon.currentAmmo >= weapon.maxAmmo)
            return false;

        weapon.currentAmmo = Mathf.Min(weapon.currentAmmo + amount, weapon.maxAmmo);
        return true;
    }

    public WeaponData GetCurrentWeaponData()
    {
        foreach (var weapon in weapons)
        {
            if (weapon.type == CurrentWeapon)
                return weapon;
        }
        return null;
    }

    private Transform GetCurrentWeaponTransform()
    {
        return GetCurrentWeaponData()?.model?.transform;
    }
}
