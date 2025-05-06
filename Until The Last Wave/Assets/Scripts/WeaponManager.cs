using UnityEngine;

public enum WeaponType { Pistol, Shotgun, Rifle }

public class WeaponManager : MonoBehaviour
{
    public GameObject pistolObject;
    public GameObject shotgunObject;
    public GameObject rifleObject;

    [Header("Weapon Animators")]
    public Animator pistolAnimator;
    public Animator shotgunAnimator;
    public Animator rifleAnimator;
    private bool isAiming = false;

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

        switch (CurrentWeapon)
        {
            case WeaponType.Pistol:
                pistolAnimator?.SetBool("IsAiming", isAiming);
                break;
            case WeaponType.Shotgun:
                shotgunAnimator?.SetBool("IsAiming", isAiming);
                break;
            case WeaponType.Rifle:
                rifleAnimator?.SetBool("IsAiming", isAiming);
                break;
        }
    }



}
