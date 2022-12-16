using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield")]
public class ShieldData : Item
{
    [Header("Info")]
    public new string name;

    [Header("Defence")]
    public int health;
    public int strength;
    public float rechargeTime;

}