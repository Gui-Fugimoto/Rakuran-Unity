using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "Damage", menuName = "DamageParameter")]

public class DamageParameter : ScriptableObject
{
    public WeaponType weaponType;


    public int Dano;
    public int DanoPesado;

   
}
