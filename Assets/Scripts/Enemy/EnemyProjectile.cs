using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float throwspeed;
    PlayerController playerController;
    public float damage;
    void Start()
    {       

        playerController = FindObjectOfType<PlayerController>();
        if (playerController.flipped == true)
        {
            throwspeed = throwspeed * -1;
        }

        if (playerController.flipped == false)
        {
            throwspeed = throwspeed * +1;
        }
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(throwspeed * Time.deltaTime, 0, 0);
        transform.Rotate(0, 0, 300 * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthController>().TakeDamage(damage);
        }
    }
}
