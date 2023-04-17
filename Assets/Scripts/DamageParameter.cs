using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "ParametersRaku")]

public class DamageParameter : ScriptableObject
{
    [SerializeField] int dano = default;
    [SerializeField] int danoPesado = default; 

    public int Dano => dano;
    public int DanoPesado => danoPesado;

    void EffectDOT()
    {

    }
}
