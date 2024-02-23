using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPanelScript : MonoBehaviour
{
    public GameObject HealthBar;
    public GameObject NameBar;
    [HideInInspector] public EnemyLife enemyLife;
    [HideInInspector] EnemyHealthUI enemyHealthUI;
    private State_Controller stateCtrl;
    [HideInInspector] public string bossName;
    [HideInInspector] EnemyNameUI nameUIScript;
    void Start()
    {
        HealthBar.SetActive(false);
        NameBar.SetActive(false);
        enemyHealthUI = HealthBar.GetComponent<EnemyHealthUI>();
        nameUIScript = NameBar.GetComponent<EnemyNameUI>();
        stateCtrl = FindObjectOfType<State_Controller>();
    }

    private void Update()
    {

        if (enemyLife != null && enemyLife.vida <= 0)
        {
            HideBars();
        }
        else if (enemyLife == null)
        {
            HideBars();
        }
        else if (stateCtrl.game_State != Game_States.In_Combat)
        {
            HideBars();
        }
    }

    public void Spawn()
    {
        HealthBar.SetActive(true);
        NameBar.SetActive(true);
        enemyHealthUI.enemyLifeScript = enemyLife;
        nameUIScript.bossName = bossName;
        nameUIScript.MudaNomeDisplay();
    }

    public void HideBars()
    {
        HealthBar.SetActive(false);
        NameBar.SetActive(false);
    }
}
