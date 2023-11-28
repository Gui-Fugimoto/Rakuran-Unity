using UnityEngine;

public class TeleportParkour : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] SaveFile save;
    public bool  IsSceneTrans;
    public GameObject fadeOut;

    void Start()
    {
        save = FindObjectOfType<GameController>().Save;
        IsSceneTrans = true;
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
