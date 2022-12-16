using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Sword", menuName="Weapon/Sword")]
public class SwordData : Item
{
    [Header("Info")]
    public new string name;

    [Header("Attacking")]
    public int damage;
    public float speed;
    public float chargeTime;
    public int chargeDamage;
    public float slashProjectileSpeed;
    public float knockback;
    public float range;
    public float timeToAttack;

}
