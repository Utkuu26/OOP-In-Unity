using UnityEngine;
using TMPro;

public enum WeaponType { Pistol, Shotgun, Rifle }

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Objects")]
    public GameObject pistolObject;
    public GameObject shotgunObject;
    public GameObject rifleObject;

    [Header("Weapon Animators")]
    public Animator pistolAnimator;
    public Animator shotgunAnimator;
    public Animator rifleAnimator;

    [Header("Camera Reference")]
    public Transform cameraTransform;

    [Header("ADS Timing")]
    public float adsLockDelay = 0.5f;
    private float adsTimer = 0f;
    private bool isAiming = false;
    private bool hasLockedFinalTransform = false;
    private Vector3 localOffsetPos;
    private Quaternion localOffsetRot;

    public GameObject crosshairGameObject;

    [Header("Ammo Settings")]
    public int pistolAmmo = 10;
    public int shotgunAmmo = 10;
    public int rifleAmmo = 30;
    public int maxPistolAmmo = 30;
    public int maxShotgunAmmo = 20;
    public int maxRifleAmmo = 60;

    public WeaponType CurrentWeapon { get; private set; }

    void Start()
    {
        EquipWeapon(WeaponType.Pistol);
    }

    public void EquipWeapon(WeaponType weaponType)
    {
        CurrentWeapon = weaponType;

        pistolObject.SetActive(weaponType == WeaponType.Pistol);
        shotgunObject.SetActive(weaponType == WeaponType.Shotgun);
        rifleObject.SetActive(weaponType == WeaponType.Rifle);
    }

    public void ToggleAim()
    {
        isAiming = !isAiming;
        hasLockedFinalTransform = false;
        adsTimer = 0f;

        Animator anim = GetCurrentAnimator();
        anim?.SetBool("IsAiming", isAiming);

        if (crosshairGameObject != null)
            crosshairGameObject.SetActive(!isAiming);
    }

    void Update()
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

    void LateUpdate()
    {
        if (isAiming && hasLockedFinalTransform)
        {
            Transform weaponTransform = GetCurrentWeaponTransform();
            if (weaponTransform != null && cameraTransform != null)
            {
                weaponTransform.position = cameraTransform.position + cameraTransform.rotation * localOffsetPos;
                weaponTransform.rotation = cameraTransform.rotation * localOffsetRot;
                Debug.DrawRay(weaponTransform.position, weaponTransform.forward * 10f, Color.green);
            }
        }
    }

    public bool TryAddAmmo(int amount)
    {
        switch (CurrentWeapon)
        {
            case WeaponType.Pistol:
                if (pistolAmmo >= maxPistolAmmo) return false;
                Debug.Log($"Ammo before: {pistolAmmo}");
                pistolAmmo = Mathf.Min(pistolAmmo + amount, maxPistolAmmo);
                Debug.Log($"Ammo after: {pistolAmmo}");
                return true;

            case WeaponType.Shotgun:
                if (shotgunAmmo >= maxShotgunAmmo) return false;
                shotgunAmmo = Mathf.Min(shotgunAmmo + amount, maxShotgunAmmo);
                return true;

            case WeaponType.Rifle:
                if (rifleAmmo >= maxRifleAmmo) return false;
                rifleAmmo = Mathf.Min(rifleAmmo + amount, maxRifleAmmo);
                return true;
        }

        return false;
    }


    private Transform GetCurrentWeaponTransform()
    {
        return CurrentWeapon switch
        {
            WeaponType.Pistol => pistolObject.transform,
            WeaponType.Shotgun => shotgunObject.transform,
            WeaponType.Rifle => rifleObject.transform,
            _ => null
        };
    }

    private Animator GetCurrentAnimator()
    {
        return CurrentWeapon switch
        {
            WeaponType.Pistol => pistolAnimator,
            WeaponType.Shotgun => shotgunAnimator,
            WeaponType.Rifle => rifleAnimator,
            _ => null
        };
    }

    public int GetAmmo()
    {
        return CurrentWeapon switch
        {
            WeaponType.Pistol => pistolAmmo,
            WeaponType.Shotgun => shotgunAmmo,
            WeaponType.Rifle => rifleAmmo,
            _ => 0
        };
    }
}
