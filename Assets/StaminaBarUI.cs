using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] Image StaminaBar;
    void Start()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        StaminaBar.fillAmount = controller.currentStamina/controller.maxStamina;
    }
}
