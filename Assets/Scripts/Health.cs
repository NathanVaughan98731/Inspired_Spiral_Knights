using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private Slider healthBar;

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
        if (healthBar != null)
        {
            //healthBar.localScale = new Vector3(this.health/100, 0.2f, 1);
            //healthBar.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y + 1f, transform.position.z);
            //healthBar.transform.LookAt(healthBar.transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
            healthBar.maxValue = MAX_HEALTH;
            healthBar.value = health;
        }
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage.");
        }
        if (health - amount <= 0)
        {
            this.health = 0;
        }
        else
        {
            this.health -= amount;
        }
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
