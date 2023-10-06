using UnityEngine;

public class ArcadeCameraFollow : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Cube");
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z - 10);
    }
}
