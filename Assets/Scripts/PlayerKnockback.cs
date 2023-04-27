using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    public float knockbackForce = 10f;
    
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            /*
            Rigidbody enemyRigidbody = col.gameObject.GetComponent<Rigidbody>();
            Vector3 knockbackDirection = new Vector3(knockbackForce, knockbackForce, 0f);
            enemyRigidbody.AddForce(knockbackDirection, ForceMode.Impulse);
            Debug.Log("dota)");
            */
            Rigidbody enemyRigidbody = col.gameObject.GetComponent<Rigidbody>();
            Vector3 knockbackDirection = col.contacts[0].normal;
            knockbackDirection.z = 0f; 
            knockbackDirection.Normalize();
            knockbackDirection += Vector3.up * 0.5f;
            knockbackDirection += Vector3.forward * 0.5f; 
            enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            Debug.Log("dota)");
        }
    }
}
