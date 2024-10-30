using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Параметры оружия
    public int maxBullets = 30;
    public int bulletsInMagazine = 30;
    public float timeBetweenShots = 0.1f;
    public float reloadTime = 1.5f;
    public bool isReloading = false;

    private float shotTimer = 0.0f;

    // Метод для выстрела
    private void Shoot()
    {
        if (bulletsInMagazine > 0 && !isReloading)
        {
            bulletsInMagazine--;

            shotTimer = 0.0f;
        }
        else if (bulletsInMagazine <= 0 && !isReloading)
        {
            Reload();
        }
    }

    private void Update()
    {
        shotTimer += Time.deltaTime;

        bool readyToShoot = shotTimer >= timeBetweenShots;

        if (Input.GetMouseButton(0) && readyToShoot)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            Reload();
        }
    }

    // Метод для перезарядки
    private void Reload()
    {
        if ((bulletsInMagazine < maxBullets && bulletsInMagazine > 0) || bulletsInMagazine == 0)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private System.Collections.IEnumerator ReloadCoroutine()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        bulletsInMagazine = maxBullets;
        isReloading = false;
    }
}
