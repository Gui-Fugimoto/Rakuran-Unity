using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "ItemParameter")]
public class ItemParameter : ScriptableObject
{

    public GameObject ItemMundo = default;
    public int Vida;
    public int Veneno;
    public Sprite Icon;
    public bool Consumivel;
    public Effect Effect;
    public Sprite EffectIcon;
    public WeaponType weaponType;
    public Forge Forge;
    public float damage;
    
    [TextArea(3,10)]
    public string Desc;
    //add elemental type, damage type, base damage value, etc.

}
public enum Effect
{
    None,
    Resist,
    Speed,
    OverTime,
    Invis,
}
public enum Forge
{ 
    None,
    TreeZephir,
    TreeAlhunz, 
    TreeOptional,
}
public enum WeaponType
{
    None,
    Hammer,
    Sword,
    Ranged,
    Arcane,
    Polearm,
   
}

