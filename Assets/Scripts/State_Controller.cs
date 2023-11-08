using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Game_States
{
    In_Combat,
    In_Explore,
    In_Cutscene,
    In_Fixed,
    In_Inventory,
    In_Searching

}
public class State_Controller : MonoBehaviour
{

    public Game_States game_State;


    // Update is called once per frame
    void Update()
    {
        switch (game_State)
        {
            case Game_States.In_Combat:
                break;
            case Game_States.In_Explore:
                Exploring();
                break;
            case Game_States.In_Cutscene:
                break;
            case Game_States.In_Fixed:
                break;
            case Game_States.In_Inventory:
                break;
            case Game_States.In_Searching:
                break;
        }
                       

    }


    void Exploring()
    {
        /*
        explorationCamera.SetActive(true);
        combatCamera.SetActive(false);
        */
    }
}
