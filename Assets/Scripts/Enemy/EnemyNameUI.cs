using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyNameUI : MonoBehaviour
{
    public TMP_Text bossNameDisplay;
    [HideInInspector]public string bossName;


    public void MudaNomeDisplay()
    {
        bossNameDisplay.text = bossName;
    }
}
