using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Punch", menuName = "Weapon/Punch")]
public class PunchData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Attacking")]
    public int damage;
    public float speed;
    public float knockback;
    public float timeToAttack;
}
