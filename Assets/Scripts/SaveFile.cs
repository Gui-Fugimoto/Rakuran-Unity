using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Save", menuName = "SaveFile")]
public class SaveFile : ScriptableObject
{
    public Vector3 CPpos;
    public List<ItemParameter> Invsave = new List<ItemParameter>();
    public int CScene;
    
    public ItemParameter Arma1;
    public ItemParameter Arma2;
    
    public ItemParameter QuickSlot;
    public ItemParameter QuickSlot1;
    public ItemParameter QuickSlot2;
    public ItemParameter QuickSlot3;

    public List<ItemParameter> ChestSaver = new List<ItemParameter>();

}
