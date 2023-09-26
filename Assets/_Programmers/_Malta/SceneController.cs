using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        if(GameManager.instance.inventory != null && GameManager.instance.playerLogic != null)
        {
            GameManager.instance.playerLogic.SaveItens();
        }
        SceneManager.LoadScene(scene);
    }
}
