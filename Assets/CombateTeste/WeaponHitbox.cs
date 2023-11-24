using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponHitbox : MonoBehaviour
{
    public float baseDamage;
    public float damage;
    BoxCollider triggerBox;
    public WeaponType weaponType;

    public float knockbackForce = 0;
    public float knockDuration = 0;
    public Vector3 knockDirection;

    public AudioClip audioClip;

    public bool flipLeft;
    private void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
        triggerBox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
    
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("atingiu inimigo");
            other.GetComponent<EnemyLife>().Damage(damage);
            
            if (flipLeft == false)
            {
                other.gameObject.GetComponent<EnemyNavMeshAgent>().Knockback(knockDirection, knockbackForce, knockDuration);
            }
            else if(flipLeft == true)
            {
                other.gameObject.GetComponent<EnemyNavMeshAgent>().Knockback(-knockDirection, knockbackForce, knockDuration);
            }
            
        }
        
    }

    public void EnableTriggerBox()
    {
        triggerBox.enabled = true;
    }
    public void DisableTriggerBox()
    {
        triggerBox.enabled = false;
    }


    
}
