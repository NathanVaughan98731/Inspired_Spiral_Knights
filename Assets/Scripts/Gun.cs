using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private TextMeshProUGUI ammoDisplay;
    [SerializeField] private TextMeshProUGUI weaponNameDisplay;
    public float bulletSpeed = 10;

    float timeSinceLastShot;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }


    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    public void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot() && this.gameObject.activeSelf)
            {
                var bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
                bullet.GetComponent<Bullet>().setBulletDamage(gunData.damage);
                bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed;
                //if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance))
                //{
                //    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                //    damageable?.Damage(gunData.damage);
                //}
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
        else
        {
            StartReload();
        }        
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (ammoDisplay != null)
        {
            ammoDisplay.SetText(gunData.currentAmmo + " / " + gunData.magSize);
        }
        if (weaponNameDisplay != null)
        {
            weaponNameDisplay.SetText(gunData.name);
        }


        //Debug.DrawRay(muzzle.position, muzzle.forward);
    }

    private void OnGunShot()
    {

    }
}
