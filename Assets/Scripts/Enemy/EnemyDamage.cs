using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    
    public float knockbackForce = 10f;
    public float knockTime = 1f;
    float damage;
    public SpriteRenderer enemySpriteRend;
    
    //variaveis que definem a posicao da hitbox estao no EnemyAi script.
    //tem que criar uma função takedamage no player.
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //other.PlayerTakeDamage();
        if (enemySpriteRend.flipX == false)
        {
            if (other.gameObject.tag == "Player")
            {
                Vector3 direction = transform.position - other.transform.position;
                other.gameObject.GetComponent<PlayerController>().Knockback(direction, knockbackForce, knockTime);
                other.gameObject.GetComponent<PlayerHealthController>().TakeDamage(1);
                
            }
        }
        else if (enemySpriteRend.flipX == true)
        {
            if (other.gameObject.tag == "Player")
            {
                Vector3 direction = other.transform.position - transform.position;
                other.gameObject.GetComponent<PlayerController>().Knockback(direction, knockbackForce, knockTime);
                other.gameObject.GetComponent<PlayerHealthController>().TakeDamage(1);
                Debug.Log("dota");
            }
        }
    }
}
