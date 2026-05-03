using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSystem : MonoBehaviour
{
    #region General Variables

    [Header("General References")]
    [SerializeField] Camera fpsCam;
    [SerializeField] Transform ShootPoint;
    [SerializeField] LayerMask impactLayer;
    RaycastHit hit;

    [Header("Weapon Parameters")]
    [SerializeField] int damage = 10;
    [SerializeField] float range = 100;
    [SerializeField] float spread = 0;
    [SerializeField] float shootingColldown = 0.2f;
    [SerializeField] float reloadTime = 1.5f;
    [SerializeField] bool allowButtonHold = false;

    [Header("Bullet Management")]
    [SerializeField] int amoSize = 30;
    [SerializeField] int bulletsPerTap = 1;
    [SerializeField] int bulletsLeft;

    [Header("FeedBack references")]
    [SerializeField] GameObject impactEffect;

    [Header(" Muzzle Flash")]
    [SerializeField] GameObject muzzleFlashPrefab;

    [Header(" Sonido disparo")]
    [SerializeField] AudioSource shootAudioSource;
    [SerializeField] AudioClip shootSound;

    [Header(" Recoil Cámara")]
    [SerializeField] float recoilX = 2f;
    [SerializeField] float recoilY = 1f;
    [SerializeField] float recoilReturnSpeed = 5f;
    [SerializeField] float recoilSnappiness = 10f;

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    [Header("Dev - Gun State Bools")]
    [SerializeField] bool shooting;
    [SerializeField] bool canShoot;
    [SerializeField] bool reloading;

    #endregion

    private void Awake()
    {
        bulletsLeft = amoSize;
        canShoot = true;
    }

    void Update()
    {
        // DISPARO
        if (canShoot && shooting && !reloading && bulletsLeft > 0)
        {
            StartCoroutine(ShootRoutine());
        }

        //  RECOIL UPDATE (SIEMPRE ACTIVO)
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, recoilReturnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, recoilSnappiness * Time.deltaTime);
        fpsCam.transform.localRotation = Quaternion.Euler(currentRotation);
    }

    IEnumerator ShootRoutine()
    {
        canShoot = false;

        if (!allowButtonHold) shooting = false;

        for (int i = 0; i < bulletsPerTap; i++)
        {
            if (bulletsLeft <= 0) break;

            Shoot();
            bulletsLeft--;
        }

        yield return new WaitForSeconds(shootingColldown);
        canShoot = true;
    }

    void Shoot()
    {
        //  SONIDO
        if (shootAudioSource && shootSound)
            shootAudioSource.PlayOneShot(shootSound);

        //  MUZZLE FLASH
        if (muzzleFlashPrefab != null && ShootPoint != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, ShootPoint.position, ShootPoint.rotation);
            Destroy(flash, 0.15f);
        }

        //  RECOIL
        ApplyRecoil();

        Vector3 direction = fpsCam.transform.forward;

        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);

        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, impactLayer))
        {
            Debug.Log(hit.collider.name);

            //  IMPACTO
            if (impactEffect != null)
            {
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 0.5f);
            }

            //  ENEMIGO
            if (hit.collider.CompareTag("enemy"))
            {
                Destroy(hit.collider.gameObject);
            }

            //  ORBE
            if (hit.collider.CompareTag("Esfera"))
            {
                Destruible1 destruible = hit.collider.GetComponent<Destruible1>();

                if (destruible != null)
                {
                    destruible.Activar();
                }

                Destroy(hit.collider.gameObject);
            }
        }
    }

    void ApplyRecoil()
    {
        targetRotation += new Vector3(
            -recoilX,
            Random.Range(-recoilY, recoilY),
            0
        );
    }

    IEnumerator ReloadRoutine()
    {
        reloading = true;

        yield return new WaitForSeconds(reloadTime);
        bulletsLeft = amoSize;
        reloading = false;
    }

    void Reload()
    {
        if (bulletsLeft < amoSize && !reloading)
        {
            StartCoroutine(ReloadRoutine());
        }
    }

    #region Input Methods

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (allowButtonHold)
        {
            shooting = context.ReadValueAsButton();
        }
        else
        {
            if (context.performed) shooting = true;
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed) Reload();
    }

    #endregion
}