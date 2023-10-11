using UnityEngine;

public class ArcadeGameManager : MonoBehaviour
{
    public static ArcadeGameManager instance;
    public ArcadePlayer player;
    private void Start()
    {
        instance = this;
    }
}
