using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    public static Action slashInput;
    public static Action chargeInput;
    public static Action countChargeTime;
    public static Action chargeAttackInput;

    [SerializeField] private KeyCode chargeKey;

    private int swingOnce = 0;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (swingOnce == 0)
            {
                swingOnce = 1;
                slashInput?.Invoke();
            }
            countChargeTime?.Invoke();
        }
        if (Input.GetMouseButtonUp(0))
        {
            chargeInput?.Invoke();
            swingOnce = 0;
        }
    }
}
