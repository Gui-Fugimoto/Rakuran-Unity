using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIdent : MonoBehaviour
{
    public PlayerCombat playerCombat;
    [SerializeField] Image Weapon1;
    [SerializeField] Sprite Martelo;
    [SerializeField] Sprite Espada;


    private void Start()
    {
        playerCombat = FindObjectOfType<PlayerCombat>();
    }
    // Update is called once per frame
    void Update()
    {
        if(playerCombat.mainWeapon.item.weaponType == WeaponType.Sword && playerCombat.mainHand == true)
        {
            Weapon1.sprite = Espada;
        }

        if (playerCombat.mainWeapon.item.weaponType == WeaponType.Hammer && playerCombat.mainHand == true)
        {
            Weapon1.sprite = Martelo;
        }

        if (playerCombat.offhandWeapon.item.weaponType == WeaponType.Sword && playerCombat.mainHand == false)
        {
            Weapon1.sprite = Espada;
        }
        
        if (playerCombat.offhandWeapon.item.weaponType == WeaponType.Hammer && playerCombat.mainHand == false)
        {
            Weapon1.sprite = Martelo;
        }
    }
}
