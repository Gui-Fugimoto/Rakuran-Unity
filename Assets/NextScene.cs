using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    // Start is called before the first frame update
    public SaveFile save;
    public GameController controller;
    public int NextSceneIndex;
    public Inventory inventory;
    public Vector3 NextSceneSpawnPos;
    
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        save = controller.Save;
        inventory = FindObjectOfType<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("passouPlayer");
            save.Invsave = new List<ItemParameter>(inventory.itens);
            SceneManager.LoadScene(NextSceneIndex);
            save.CPpos = NextSceneSpawnPos;
        }
    }
}
