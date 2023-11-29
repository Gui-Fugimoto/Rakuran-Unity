using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimCheck : MonoBehaviour
{
    Animator anim;
    string attackAnim;
    AnimEventHitbox animEventHitbox;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attackAnim = "Attack";
        animEventHitbox = GetComponent<AnimEventHitbox>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPlaying(anim, attackAnim))
        {
            animEventHitbox.DisableHitboxSimple();
            //animEventHitbox.AnimDisableSpecialHitbox();
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
}
