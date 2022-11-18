using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private int damage = 3;
    [SerializeField] private int punch = 1;

    private Vector3 forceDirection;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);
            Vector3 mousePos = Input.mousePosition;

            // Get the direction of the attack from the player to the mouse cursor
            forceDirection = GetWorldPositionOnPlane(Input.mousePosition, 0) - gameObject.GetComponentInParent<Transform>().position;

            Vector3 n_forceDirection = forceDirection.normalized;
            Debug.Log(n_forceDirection);
            Debug.DrawLine(gameObject.GetComponentInParent<Transform>().position, gameObject.GetComponentInParent<Transform>().position + n_forceDirection, Color.red);
            collider.GetComponent<Rigidbody>().AddForce(new Vector3(n_forceDirection.x, 0, n_forceDirection.z) * punch, ForceMode.Impulse);

        }
    }

    public static Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float height)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane plane = new Plane(Vector3.up, new Vector3(ray.origin.x, height, ray.origin.z));

        float distance;
        plane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
