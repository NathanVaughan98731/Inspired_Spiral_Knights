using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    public static Action slashInput;
    public static Action deactivate;
    public static Action chargeInput;

    [SerializeField] private KeyCode chargeKey;

    private bool charging;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            slashInput?.Invoke();
        }
        if (Input.GetMouseButton(0))
        {
            chargeInput?.Invoke();
        }
        if (Input.GetMouseButtonUp(0))
        {
            slashInput?.Invoke();
        }


    }
}
