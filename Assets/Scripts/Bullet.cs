using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    [SerializeField] private float damage;

    public void setBulletDamage(float gunDataDamage)
    {
        this.damage = gunDataDamage;
    }

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
        //    damageable?.Damage(gunData.damage);
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        damageable?.Damage(damage);
        Destroy(gameObject);
    }
}
