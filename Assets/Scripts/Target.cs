using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;
    public void Damage(int damage)
    {
        this.health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
