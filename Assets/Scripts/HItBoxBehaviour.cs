using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItBoxBehaviour : MonoBehaviour
{
    [SerializeField] PlayerAttack RefDamage;
    private int Str;

    // Start is called before the first frame update
    void Start()
    {
        //RefDamage = GetComponent<PlayerAttack>();

      
    }

    // Update is called once per frame
    void Update()
    {
        Str = RefDamage.Dano;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {

            other.gameObject.SendMessage("Damage", Str);

        }
    }
}

