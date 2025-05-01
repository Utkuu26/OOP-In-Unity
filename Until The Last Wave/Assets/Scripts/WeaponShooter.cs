using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    public WeaponManager weaponManager;
    public Camera playerCamera;

    public float pistolDamage = 5f;
    public float shotgunDamage = 10f;
    public float rifleDamage = 3f;

    public float pistolFireRate = 0.5f;
    public float shotgunFireRate = 1f;
    public float rifleFireRate = 0.1f;

    private float nextFireTime = 0f;

    public AudioSource audioSource;
    public AudioClip pistolSound;
    public AudioClip shotgunSound;
    public AudioClip rifleSound;

    public ParticleSystem pistolFlash;
    public ParticleSystem shotgunFlash;
    public ParticleSystem rifleFlash;


    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        float damage = 0f;
        float fireRate = 0f;
        AudioClip fireClip = null;

        switch (weaponManager.CurrentWeapon)
        {
            case WeaponType.Pistol:
                damage = pistolDamage;
                fireRate = pistolFireRate;
                fireClip = pistolSound;
                if (pistolFlash != null) pistolFlash.Play();
                break;
            case WeaponType.Shotgun:
                damage = shotgunDamage;
                fireRate = shotgunFireRate;
                fireClip = shotgunSound;
                if (shotgunFlash != null) shotgunFlash.Play();
                break;
            case WeaponType.Rifle:
                damage = rifleDamage;
                fireRate = rifleFireRate;
                fireClip = rifleSound;
                if (rifleFlash != null) rifleFlash.Play();
                break;
        }

        nextFireTime = Time.time + fireRate;

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f); //Raycast çizdirme

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
        {
            Debug.Log("Hit: " + hitInfo.collider.name);

            EnemyHealth enemy = hitInfo.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        if (fireClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireClip);
        }
    }
}
