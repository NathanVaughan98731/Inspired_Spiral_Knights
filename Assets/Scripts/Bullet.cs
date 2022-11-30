using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    [SerializeField] private int damage;
    public GameObject damageText;

    public void setBulletDamage(int gunDataDamage)
    {
        this.damage = gunDataDamage;
    }

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (collision.gameObject.GetComponent<Bullet>() == null)
        //{
        //    IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        //    damageable?.Damage(damage);
        //    DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        //    indicator.SetDamageText(damage);
        //    Destroy(gameObject);
        //}
        Debug.Log(other.gameObject);
        if (other.gameObject.GetComponent<Bullet>() == null && other.tag != "Player")
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable?.Damage(damage);
            DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
            indicator.SetDamageText(damage);
            Destroy(gameObject);
        }

    }
}
