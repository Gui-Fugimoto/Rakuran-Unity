using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class potionThrow : MonoBehaviour
{
    public float throwspeed;
    [SerializeField] float fallDistance;
    float PotionDamage;
    public ItemParameter Item;
    [SerializeField] SpriteRenderer Potion;
    PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        Potion.sprite = Item.Icon;
        playerController = FindObjectOfType<PlayerController>();

        if (playerController.flipped == true)
        {
            throwspeed = throwspeed * -1;
        }

        if (playerController.flipped == false)
        {
            throwspeed = throwspeed * +1;
        }

        PotionDamage = Item.Veneno;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(throwspeed * Time.deltaTime, 0, 0);
        transform.Rotate(0,0, 300 * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("VENENOU");
            other.GetComponent<EnemyLife>().Damage(PotionDamage);
            Destroy(gameObject, 0.1f);

            if(Item.Effect == Effect.OverTime)
            {
                Debug.Log("VENENOU Pelo Tempo");
                other.GetComponent<EnemyLife>().DamageOT(PotionDamage);
                Destroy(gameObject, 0.1f);
            }

            if(Item.Effect == Effect.Resist)
            {
                Debug.Log("Fraqueceu");
                other.GetComponent<EnemyLife>().ResistPerda();
                Destroy(gameObject, 0.1f);
            }

            if(Item.Effect == Effect.Speed)
            {
                Debug.Log("Lentou");
                other.GetComponent<EnemyLife>().Slow();
                Destroy(gameObject, 0.1f);
            }

            if(Item.Effect == Effect.Invis)
            {
                Debug.Log("Stunou");
                other.GetComponent<EnemyNavMeshAgent>().Stun(Item.Veneno);
            }
        }
    }


}
