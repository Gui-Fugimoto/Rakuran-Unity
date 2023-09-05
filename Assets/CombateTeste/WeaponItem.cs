using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Hammer,
    Sword,
    Ranged,
    Arcane,
    Polearm,
    None
}


[CreateAssetMenu(menuName = "Weapon/WeaponItem")]
public class WeaponItem : ScriptableObject
{
    public WeaponType weaponType;
    public float damage;
    //add elemental type, damage type, base damage value, etc.
}
