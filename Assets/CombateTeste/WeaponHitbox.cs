using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponHitbox : MonoBehaviour
{
    public float baseDamage;
    public float damage;
    SphereCollider triggerBox;
    public WeaponType weaponType;

    public float knockbackForce = 0;
    public float knockDuration = 0;
    public Vector3 knockDirection;

    private void Start()
    {
        triggerBox = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("atingiu inimigo");
            other.GetComponent<EnemyLife>().Damage(damage);
        }
        if (other.gameObject.tag == "Enemy")
        {
            Vector3 direction = transform.position - other.transform.position;
            other.gameObject.GetComponent<EnemyAI>().Knockback(direction, knockbackForce, knockDuration);

        }


        if (other.gameObject.tag == "Enemy")
        {
            Vector3 direction = other.transform.position - transform.position;
            other.gameObject.GetComponent<EnemyAI>().Knockback(direction, knockbackForce, knockDuration);

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