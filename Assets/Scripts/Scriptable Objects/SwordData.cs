using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Sword", menuName="Weapon/Sword")]
public class SwordData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Attacking")]
    public int damage;
    public float speed;
    public float chargeTime;
    public float chargeDamage;
    public float knockback;
    public float range;
    public float timeToAttack;

}
