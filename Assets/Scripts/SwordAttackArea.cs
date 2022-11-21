using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackArea : MonoBehaviour
{
    [SerializeField] Sword sword;
    public GameObject damageText;

    private Vector3 forceDirection;

    private void OnTriggerEnter(Collider collider)
    {
        IDamageable damageable = collider.gameObject.GetComponentInParent<IDamageable>();
        damageable?.Damage(sword.swordData.damage);
        DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(sword.swordData.damage);

        Vector3 mousePos = Input.mousePosition;
        forceDirection = GetWorldPositionOnPlane(Input.mousePosition, 0) - gameObject.GetComponentInParent<Transform>().position;
        Vector3 n_forceDirection = forceDirection.normalized;
        
        collider.GetComponentInParent<Rigidbody>().AddForce(new Vector3(n_forceDirection.x, 0, n_forceDirection.z) * sword.swordData.knockback, ForceMode.Impulse);
    }

    public static Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float height)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane plane = new Plane(Vector3.up, new Vector3(ray.origin.x, height, ray.origin.z));

        float distance;
        plane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    public void Update()
    {
        
    }
}
