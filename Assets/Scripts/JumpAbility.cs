using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class JumpAbility : Ability
{
    public float jumpForce;

    public override void Activate(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();
        Rigidbody rb = parent.GetComponent<Rigidbody>();

        rb.velocity += new Vector3(0, jumpForce, 0);
    }
}
