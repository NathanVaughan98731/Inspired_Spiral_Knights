using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{
    [SerializeField] private ShieldData shieldData;

    public GameObject shield;
    public SphereCollider shieldCollider;
    public int health;
    public int strength;
    public float rechargeTime;

    public bool shieldActivated;
    public bool shieldBroken;
    public float timeBroken;

    private void Awake()
    {
        health = shieldData.health;
        strength = shieldData.strength;
        rechargeTime = shieldData.rechargeTime;
        shieldActivated = false;
        shieldBroken = false;
        timeBroken = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldBroken)
        {
            timeBroken += Time.deltaTime;
            if (timeBroken >= rechargeTime)
            {
                timeBroken = 0;
                shieldBroken = false;
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (!shieldActivated && !shieldBroken)
            {
                health = shieldData.health;
                ActivateShield();
            }
        }
        else
        {
            if (shieldActivated)
            {
                DeactivateShield();
            }
        }
    }

    private void ActivateShield()
    {
        shieldActivated = true;
        shield.SetActive(true);
    }

    private void DeactivateShield()
    {
        shieldActivated = false;
        shield.SetActive(false);
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            shieldBroken = true;
            DeactivateShield();
        }

    }
}
