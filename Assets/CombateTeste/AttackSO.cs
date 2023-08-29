using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Attacks/Normal Attack")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOV;
    public float damageMultiplier;

    //knockback
    public float kbForce;
    public float kbDuration;
    public Vector3 kbDirection;

    //Attack Duration
    public float animSpeed;
    public float endLag;
    public float startUp;
    
}
