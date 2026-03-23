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


    [Header("Dev - Gun State Bools")]
    [SerializeField] bool shooting;
    [SerializeField] bool canShoot;
    [SerializeField] bool reloading;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    #endregion
    private void Awake()
    {
        bulletsLeft = amoSize;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && shooting && !reloading && bulletsLeft > 0)
        {

            StartCoroutine(ShootRoutine());


        }
    }


    IEnumerator ShootRoutine()
    {



        canShoot = false;
        if (!allowButtonHold) shooting = false;
        for (int i =0; i < bulletsPerTap; i++)
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
        Vector3 direction = fpsCam.transform.forward;

        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);

        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, impactLayer))
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("enemy"))
            {
                HealthEnemy healthEnemy = hit.collider.GetComponent<HealthEnemy>();
                healthEnemy.TakeDamage(damage);
            }

            if (hit.collider.CompareTag("Esfera"))
            {
                // Buscar el script destruible
                Destruible1 destruible = hit.collider.GetComponent<Destruible1>();

                if (destruible != null)
                {
                    destruible.Activar();
                }

                Destroy(hit.collider.gameObject);
            }
        }
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
