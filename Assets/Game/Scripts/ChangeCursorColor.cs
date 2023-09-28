using UnityEngine;


public class ChangeCursorColor : MonoBehaviour
{
    public Texture2D[] customCursorTexture; // Textura personalizada para o cursor
    public Vector2 cursorHotspot = Vector2.zero; // Ponto quente do cursor (ponto onde ele interage com objetos)
    private RaycastHit hit;
    private Ray ray;
    private bool isCloseEnough;
    [SerializeField]private Camera mainCamera;
    private LayerMask layer;
    private float delay =0;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void FixedUpdate()
    {
        if (delay >= 1)
        {
            Raycast();
            ChangeMouse();
            delay = 0;
        }
        else
        {
            delay += 0.4f;
        }
    }
    private void Raycast()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
    }
    public void ChangeMouse()
    {
        Cursor.SetCursor(customCursorTexture[textureID()], cursorHotspot, CursorMode.Auto);        
    }
    private int textureID()
    {
        layer = hit.collider.gameObject.layer;
        if (Vector3.Distance(GameManager.instance.playerLogic.transform.position, hit.collider.transform.position) < GameManager.instance.playerLogic.atackRange)
        {
            isCloseEnough = true;
        }
        else
        {
            isCloseEnough = false;
        }

        switch (layer)
        {
            case 6:
                //Ore
                if (isCloseEnough)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            case 7:
                //Craft
                if (isCloseEnough)
                {
                    return 3;
                }
                else
                {
                    return 4;
                }
            case 8:
                //Storage
                if (isCloseEnough)
                {
                    return 5;
                }
                else
                {
                    return 6;
                }
                case 9:
                if (isCloseEnough)
                {
                    return 7;
                }
                else
                {
                    return 8;
                }
            default:
                //Default Cursor
                return 0;
        }

    }
}
