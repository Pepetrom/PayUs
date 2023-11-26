using UnityEngine;

public class Minimap : MonoBehaviour
{
    public RenderTexture targetTexture;
    public Transform player;

    void Start()
    {
        GetComponent<Camera>().targetTexture = targetTexture;
    }
    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
