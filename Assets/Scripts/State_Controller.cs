using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Game_States
{
    In_Combat,
    In_Explore,
    In_Cutscene,
    In_Fixed,
    In_Inventory

}
public class State_Controller : MonoBehaviour
{
    public GameObject player;

    public Game_States game_State;
    public GameObject explorationCamera;
    public GameObject combatCamera;
    public GameObject inventoryCamera;
    public GameObject cutsceneCamera;
    [HideInInspector]public GameObject fixedCamera;

    private Inventory inventory_script;
    private PlayerHealthController phc_script;
    public bool inFixedArea = false;
    private void Start()
    {
        inventory_script = player.GetComponent<Inventory>();
        phc_script = player.GetComponent<PlayerHealthController>();
    }
    void Update()
    {
        DefineCamera();
        switch (game_State)
        {
            case Game_States.In_Combat:
                CombatCamera();
                break;
            case Game_States.In_Explore:
                ExploringCamera();
                break;
            case Game_States.In_Cutscene:
                CutsceneCamera();
                break;
            case Game_States.In_Fixed:
                FixedCamera();
                break;
            case Game_States.In_Inventory:
                InventoryCamera();
                break;
            
        }
                       

    }


    void ExploringCamera()
    {
        //ativar explorationCamera e desativar as outras

        //explorationCamera.SetActive(true);
        //combatCamera.SetActive(false);
        
    }

    void CombatCamera()
    {
        //ativar combatCamera e desativar as outras

        //explorationCamera.SetActive(false);
        //combatCamera.SetActive(true);
    }

    void InventoryCamera()
    {
        //ativar inventoryCamera e desativar as outras
    }

    void CutsceneCamera()
    {
        
        if(cutsceneCamera != null)
        {
            //ativar cutsceneCamera e desativar as outras (nesse caso é mais pra evitar bugs)
        }
    }

    void FixedCamera()
    {
        
        if(fixedCamera != null)
        {
            //ativar fixedCamera e desativar as outras
            //a fixedcamera deve ser definida pelo gameobject que contem o script FixedCameraArea
        }
    }

    public void DefineCamera()
    {
        if(game_State != Game_States.In_Cutscene)
        {
            if (inventory_script.Aberto == true)
            {
                game_State = Game_States.In_Inventory;
            }
            else
            {
                if (phc_script.inCombat == true)
                {
                    game_State = Game_States.In_Combat;
                }
                else
                {
                    if (inFixedArea == true)
                    {
                        game_State = Game_States.In_Fixed;
                    }
                    else
                    {
                        game_State = Game_States.In_Explore;
                    }
                    
                }
            }
        }
        
    }
}
