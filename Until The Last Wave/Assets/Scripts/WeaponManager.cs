using UnityEngine;

public enum WeaponType { Pistol, Shotgun, Rifle }

public class WeaponManager : MonoBehaviour
{
    public GameObject pistolObject;
    public GameObject shotgunObject;
    public GameObject rifleObject;
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
}
