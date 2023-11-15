using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public float currentHP;
    public bool IsInv = false;
    public float maxHP;
    public GameController gameC;
    [SerializeField] SpriteRenderer playerSprite;
    public Animator anim;
    [SerializeField] int Count;
    private PlayerController playerC;
    private bool damageDelay;
    [SerializeField] int resist;
    public bool inCombat;
    private float combatTimer;
    public float combatEndDelay = 3f;
    // Start is called before the first frame update
    void Awake()
    {
        currentHP = maxHP;
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        gameC = FindObjectOfType<GameController>();
        anim = GetComponentInChildren<Animator>();
        playerC = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerDeath();
        EndCombat();
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!playerC.invulnerable && damageDelay == false)
        {
            currentHP -= (damage - resist);
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

        if(consumed.Effect == Effect.Resist)
        {
            StartCoroutine(ResistBuff());
        }

        if(consumed.Effect == Effect.Speed)
        {
            playerC.speedPotion();
        }

        if(consumed.Effect == Effect.Invis)
        {
            StartCoroutine(Invis());
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

    IEnumerator ResistBuff()
    {
        resist = +1;
        yield return new WaitForSeconds(5f);
        resist = 0;
    }

    IEnumerator Invis()
    {
        IsInv = true;
        playerSprite.color = new Color(1, 1, 1, 0.5f);
       
        yield return new WaitForSeconds(30f);
        
        playerSprite.color = new Color(1, 1, 1, 1f);
        IsInv = false;
    }

    public void EnterCombat()
    {
        combatTimer = Time.time;
        inCombat = true;
        
        
    }
    void EndCombat()
    {
        if (Time.time - combatTimer > combatEndDelay)
        {
            
            inCombat = false;
        }
    }
}
