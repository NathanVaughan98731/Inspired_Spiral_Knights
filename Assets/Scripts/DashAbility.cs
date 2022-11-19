using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();
        Rigidbody rb = parent.GetComponent<Rigidbody>();

        rb.velocity += player.PlayerMovementInput.normalized * dashVelocity;
        Debug.Log(rb.velocity);
    }
}
