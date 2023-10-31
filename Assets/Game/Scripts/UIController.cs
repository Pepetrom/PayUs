using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    public GameObject panel;
    public PlayerLogic playerLogic = null;
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }
    public void SfxVolume()
    {
        AudioManager.Instance.SfxVolume(_sfxSlider.value);
    }
    public void QuitPanel()
    {
        panel.SetActive(false);
        if (panel.activeSelf)
        {
            if (playerLogic != null)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            Time.timeScale = 1;
            panel.SetActive(false);
        }
    }
}
