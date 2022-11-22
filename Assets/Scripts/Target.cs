using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;
    [SerializeField] private ParticleSystem hitParticleSystem;
    public void Damage(int damage)
    {
        GameObject particles = Instantiate(hitParticleSystem.gameObject, this.transform);
        particles.GetComponent<ParticleSystem>().Play();

        this.health -= damage;
        if (health <= 0)
        {
            particles.gameObject.transform.parent = null;
            Destroy(gameObject);
            Destroy(particles.gameObject, 2f);
        }
        Destroy(particles.gameObject, 2f);
    }
}
