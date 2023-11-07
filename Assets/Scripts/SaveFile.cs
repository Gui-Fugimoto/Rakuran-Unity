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
}
