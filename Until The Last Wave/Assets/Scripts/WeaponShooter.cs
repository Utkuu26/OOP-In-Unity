using TMPro;
using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    public WeaponManager weaponManager;
    public Camera playerCamera;

    private float nextFireTime = 0f;

    public AudioSource audioSource;
    public AudioClip emptyFireSound;

    public TextMeshProUGUI ammoText;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
            Shoot();

        if (Input.GetMouseButtonDown(1))
            weaponManager.ToggleAim();

        ammoText.text = weaponManager.GetAmmo().ToString();
    }

    void Shoot()
    {
        if (weaponManager.GetAmmo() <= 0)
        {
            audioSource?.PlayOneShot(emptyFireSound);
            return;
        }

       

        WeaponData weaponData = weaponManager.GetCurrentWeaponData();
        if (weaponData == null) return;

        nextFireTime = Time.time + weaponData.fireRate;

        // Görsel ve ses efektleri
        weaponData.Flash?.Play();
        audioSource?.PlayOneShot(weaponData.fireSound);

        // Mermi tüket
        weaponManager.ConsumeAmmo();

        // Niþangah merkezinden ýþýn gönder
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
        {
            Debug.Log("Ray hit: " + hitInfo.collider.name);

            // Düþman kontrolü
            EnemyBase enemy = hitInfo.collider.GetComponentInParent<EnemyBase>();
            if (enemy != null)
            {
                enemy.TakeDamage(weaponData.damage);
                Debug.Log($"Enemy {enemy.gameObject.name} took {weaponData.damage} damage. Current Health: {enemy.currentHealth}");
            }
        }
    }
}
