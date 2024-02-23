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
    public GameObject fixedCamera;

    private Inventory inventory_script;
    private PlayerHealthController phc_script;
    public bool inFixedArea = false;

    public AudioSource audSourceST;
    public AudioClip combatClip;
    public AudioClip sceneClip;
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
                       
        if(game_State != Game_States.In_Combat)
        {
            if (audSourceST.clip != sceneClip)
            {
                audSourceST.clip = sceneClip;
                if (!audSourceST.isPlaying)
                {
                    audSourceST.Play();
                }

            }
        }
    }


    void ExploringCamera()
    {
        //ativar explorationCamera e desativar as outras

        combatCamera.SetActive(false);
        inventoryCamera.SetActive(false);
        fixedCamera.SetActive(false);
        cutsceneCamera.SetActive(false);
        explorationCamera.SetActive(true);
    }

    void CombatCamera()
    {
        //ativar combatCamera e desativar as outras

        explorationCamera.SetActive(false);
        inventoryCamera.SetActive(false);
        fixedCamera.SetActive(false);
        cutsceneCamera.SetActive(false);
        combatCamera.SetActive(true);

        if (audSourceST.clip != combatClip)
        {
            audSourceST.clip = combatClip;
            if (!audSourceST.isPlaying)
            {
                audSourceST.Play();
            }
            
        }
        
        
    }

    void InventoryCamera()
    {
        //ativar inventoryCamera e desativar as outras

        explorationCamera.SetActive(false);
        combatCamera.SetActive(false);
        fixedCamera.SetActive(false);
        cutsceneCamera.SetActive(false);
        inventoryCamera.SetActive(true);
    }

    void CutsceneCamera()
    {
        
        if(cutsceneCamera != null)
        {
            //ativar cutsceneCamera e desativar as outras (nesse caso é mais pra evitar bugs)

            explorationCamera.SetActive(false);
            combatCamera.SetActive(false);
            inventoryCamera.SetActive(false);
            fixedCamera.SetActive(false);
            cutsceneCamera.SetActive(true);
        }
    }

    void FixedCamera()
    {
        if (fixedCamera != null)
        {
            //ativar fixedCamera e desativar as outras
            //a fixedcamera deve ser definida pelo gameobject que contem o script FixedCameraArea

            inventoryCamera.SetActive(false);
            explorationCamera.SetActive(false);
            combatCamera.SetActive(false);
            cutsceneCamera.SetActive(false);
            fixedCamera.SetActive(true);
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
