using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTargetScript : MonoBehaviour
{
   
    private Transform targetPlayer;
    public float followSpeed = 5.0f;
    public LayerMask Ground;
   

    private void Start()
    {
        targetPlayer = GameObject.FindWithTag("Player").transform;
        StartCoroutine(Explosion());
    }
    void FixedUpdate()
    {
        if (targetPlayer != null)
        {
            
            Vector3 playerDirection = (targetPlayer.position - transform.position).normalized;

            
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, Ground))
            {
                
                Vector3 newPosition = hit.point;
                transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
            }

            
            Vector3 targetPosition = targetPlayer.position - playerDirection;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

   
    public void SetTargetPlayer(Transform player)
    {
        targetPlayer = player;
    }

    public IEnumerator Explosion()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }

    
}

