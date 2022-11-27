using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] weapons;

    [Header("Keys")]
    [SerializeField] private KeyCode[] keys;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    private int selectedWeapon;
    private float timeSinceLastSwitch;

    private void Start()
    {
        SetWeapons();
        Select(selectedWeapon);

        timeSinceLastSwitch = 0f;
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime)
            {
                selectedWeapon = i;
            }
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            Select(selectedWeapon);
        }

        timeSinceLastSwitch += Time.deltaTime;
    }

    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i);
        }

        if (keys == null) keys = new KeyCode[weapons.Length];
    }

    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == weaponIndex)
            {
                weapons[i].gameObject.SetActive(true);
                if (weapons[i].gameObject.GetComponent<Sword>() != null)
                {
                    this.GetComponentInParent<PlayerSlash>().enabled = true;
                    this.GetComponentInParent<PlayerShoot>().enabled = false;
                }
                else
                {
                    this.GetComponentInParent<PlayerShoot>().enabled = true;
                    this.GetComponentInParent<PlayerSlash>().enabled = false;
                }
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }

        timeSinceLastSwitch = 0f;

        OnWeaponSelected();
    }

    private void OnWeaponSelected()
    {
        print("Selected new weapon..");
    }
}
