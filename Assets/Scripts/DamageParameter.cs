using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "ParametersRaku")]

public class DamageParameter : ScriptableObject
{
    [SerializeField] int dano = default;
    //[SerializeField]

    public int Dano => dano;

    void EffectDOT()
    {

    }
}
