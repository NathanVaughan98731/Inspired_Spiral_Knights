using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashProjectile : MonoBehaviour
{
    public float life = 3;
    public Vector3 direction;
    [SerializeField] private int damage;
    public GameObject damageText;

    private void Update()
    {
        this.transform.position += direction;
    }

    public void setSlashDamage(int slashDamage)
    {
        this.damage = slashDamage;
    }

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponentInParent<SlashProjectile>() == null && other.tag != "Player")
        {
            IDamageable damageable = other.GetComponentInParent<IDamageable>();
            damageable?.Damage(damage);
            DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
            indicator.SetDamageText(damage);
        }
    }
}
