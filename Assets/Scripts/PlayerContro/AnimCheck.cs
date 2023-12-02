using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCheck : MonoBehaviour
{
    Animator anim;
    string attackAnim;
    PlayerCombat combatScript;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attackAnim = "Attack";
        combatScript = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlaying(anim, attackAnim))
        {
            combatScript.isAttacking = true;           
        }
        else if(!IsPlaying(anim, attackAnim))
        {            
            combatScript.isAttacking = false;
            combatScript.DisableTriggerBoxEQPW();
        }
    }

    public bool IsPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag(stateName) &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    public void AttackEvent()
    {
        combatScript.EnableTriggerBoxEQPW();
    }

    public void EndAttackEvent()
    {
        combatScript.DisableTriggerBoxEQPW();
    }
}


