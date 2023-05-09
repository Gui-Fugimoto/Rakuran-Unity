using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    public float knockbackForce = 10f;
    public float knockTime = 1f;
    public GameObject player;
    private PlayerController playerScript;
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
    }
    

    void OnCollisionEnter(Collision col)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerScript.flipped == false)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Vector3 direction = transform.position - other.transform.position;
                other.gameObject.GetComponent<EnemyAI>().Knockback(direction, knockbackForce, knockTime);
                Debug.Log("dota");
            }
        }
        else if (playerScript.flipped == true)
        {
            if (other.gameObject.tag == "Enemy")
            { 
              Vector3 direction = other.transform.position - transform.position;
              other.gameObject.GetComponent<EnemyAI>().Knockback(direction, knockbackForce, knockTime);
              
            } 
        }     
    }


    /*
    Rigidbody enemyRigidbody = col.gameObject.GetComponent<Rigidbody>();
    Vector3 knockbackDirection = new Vector3(knockbackForce, knockbackForce, 0f);
    enemyRigidbody.AddForce(knockbackDirection, ForceMode.Impulse);
    Debug.Log("dota)");
    */

    /*
    Rigidbody enemyRigidbody = col.gameObject.GetComponent<Rigidbody>();
    Vector3 knockbackDirection = col.contacts[0].normal;
    knockbackDirection.z = 0f; 
    knockbackDirection.Normalize();
    knockbackDirection += Vector3.up * 0.5f;
    knockbackDirection += Vector3.forward * 0.5f; 
    enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    Debug.Log("dota)");
    */
}
