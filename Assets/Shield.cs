using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject shield;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ActivateShield();
        }
        else
        {
            DeactivateShield();
        }
    }

    private void ActivateShield()
    {
        shield.SetActive(true);
    }

    private void DeactivateShield()
    {
        shield.SetActive(false);
    }
}
