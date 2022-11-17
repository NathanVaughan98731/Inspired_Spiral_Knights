using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    private int MAX_HEALTH = 100;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Damage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage.");
        }
        this.health -= amount;
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Heal.");
        }
        if (this.health + amount > MAX_HEALTH)
        {
            this.health = 100;
        }
        else
        {
            this.health += amount;
        }
    }
}
