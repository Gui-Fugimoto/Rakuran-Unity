using UnityEngine;

public class TeleportParkour : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] SaveFile save;
   // [SerializeField] Transform OnEmptyCheckpoint;
    public bool  IsSceneTrans;
    public GameObject fadeOut;

    void Start()
    {
        save = FindObjectOfType<GameController>().Save;
        IsSceneTrans = false;
    }
    public void Teleport()
    {
        player.transform.position = save.CPpos;
    }
    public void EndTeleport()
    {
       if(IsSceneTrans == false)
        {
            fadeOut.SetActive(false);
        }
    }
}
