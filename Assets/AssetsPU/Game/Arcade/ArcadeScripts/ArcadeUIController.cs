using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeUIController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ATV1()
    {
        SceneManager.LoadScene("ATV1");
    }
    public void Exemplo()
    {
        SceneManager.LoadScene("Lunar Lander");
    }
    public void ATV2()
    {
        SceneManager.LoadScene("ATV2");
    }
    public void ATV3()
    {
        SceneManager.LoadScene("ATV3");
    }
    public void HOME()
    {
        SceneManager.LoadScene("Home");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
