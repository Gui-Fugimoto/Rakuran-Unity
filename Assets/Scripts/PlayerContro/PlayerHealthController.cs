using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public float currentHP;
    public float maxHP;
    public GameController gameC;
    public Animator anim;
    [SerializeField] int Count;
    private PlayerController playerC;
    private bool damageDelay;
    // Start is called before the first frame update
    void Awake()
    {
        currentHP = maxHP;
        gameC = FindObjectOfType<GameController>();
        anim = GetComponentInChildren<Animator>();
        playerC = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDeath();

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!playerC.invulnerable && damageDelay == false)
        {
            currentHP -= damage;
            Debug.Log("tomou dano, vida atual " + (currentHP));
            GetComponentInParent<SimpleFlash>().Flash();
            anim.SetTrigger("Damaged");
            StartCoroutine(InvulnerableDelay());
        }
        
    }
     public void ConsumeItem(ItemParameter consumed)
    {
        if(consumed.Effect == Effect.None)
        {
            if (consumed.Vida > 00 && currentHP != maxHP)
            {
                currentHP += consumed.Vida;
            }
            if (consumed.Veneno > 00)
            {
                currentHP -= consumed.Veneno;
            }
        }

        if (consumed.Effect == Effect.OverTime)
        {
            if (consumed.Vida > 00 && currentHP != maxHP)
            {
                InvokeRepeating("OvertimeHealing", 0.0f, 1f);
                Count = consumed.Vida;
            }
            if (consumed.Veneno > 00)
            {
                InvokeRepeating("OvertimePoison", 0.0f, 1f);
            }
        }
       
    }

    void OvertimeHealing()
    {
      if(Count > 0)
      {
          currentHP += 1;
            Count -= 1;
      }
      
      if(Count == 0)
      {
          CancelInvoke("OvertimeHealing");
      }

        Debug.Log("curou");
    }

    void OvertimePoison()
    {
        if (Count > 0)
        {
            currentHP -= 1;
            Count -= 1;
        }

        if (Count == 0)
        {
            Count = 5;
            CancelInvoke("OvertimePoison");
        
        }

    }

    void PlayerDeath()
    {
        if (currentHP <= 0)
        {
            gameC.GameOver();
            
        }
    }

    IEnumerator InvulnerableDelay()
    {
        damageDelay = true;
        yield return new WaitForSeconds(0.5f);
        damageDelay = false;
    }
}
