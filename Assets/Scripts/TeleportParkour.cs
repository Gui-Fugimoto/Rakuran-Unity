using UnityEngine;

public class TeleportParkour : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] SaveFile save;
    public GameObject fadeOut;

    void Start()
    {
        save = FindObjectOfType<GameController>().Save;
    }
    public void Teleport()
    {
        player.transform.position = save.CPpos;

    }
    public void EndTeleport()
    {
        fadeOut.SetActive(false);
    }
}
