using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private int damage = 3;
    [SerializeField] private int punch = 500;

    private Vector3 forceDirection;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                forceDirection = hit.point.normalized;
                collider.GetComponent<Rigidbody>().AddForce(forceDirection * punch, ForceMode.Impulse);
                Debug.Log(forceDirection);
            }
        }
    }
}
