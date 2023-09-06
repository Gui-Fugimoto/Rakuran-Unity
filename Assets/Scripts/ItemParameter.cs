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

}
public enum Effect
{
    Vision,
    Speed,
    OverTime,
    None,
}

