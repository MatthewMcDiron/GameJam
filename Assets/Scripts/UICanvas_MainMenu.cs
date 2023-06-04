using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas_MainMenu : MonoBehaviour
{
    [SerializeField] Image StartBackgroundImage;
    [SerializeField] Image QuitBackgroundImage;
    [SerializeField] GameObject LevelSelectCanvas;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance().PlayMusic(AudioManager.Instance().MainMenuMusic);
    }

    public void OnMouseOverStartButton()
    {
        AudioManager.Instance().PlaySFX(AudioManager.Instance().MenuItemEnterSFX, "MenuItem");
        StartBackgroundImage.color = new Color(StartBackgroundImage.color.r, StartBackgroundImage.color.g, StartBackgroundImage.color.b, 0.6f);
    }

    public void OnMouseClickStartButton()
    {
        AudioManager.Instance().PlaySFX(AudioManager.Instance().MenuItemClickedSFX, "MenuItem");
        GameObject.Instantiate(LevelSelectCanvas, null);
        Destroy(gameObject);
    }

    public void OnMouseExitStartButton()
    {
        StartBackgroundImage.color = new Color(StartBackgroundImage.color.r, StartBackgroundImage.color.g, StartBackgroundImage.color.b, 1.0f);
    }

    public void OnMouseOverQuitButton()
    {
        AudioManager.Instance().PlaySFX(AudioManager.Instance().MenuItemEnterSFX, "MenuItem");
        QuitBackgroundImage.color = new Color(QuitBackgroundImage.color.r, QuitBackgroundImage.color.g, QuitBackgroundImage.color.b, 0.6f);
    }

    public void OnMouseClickQuitButton()
    {
        AudioManager.Instance().PlaySFX(AudioManager.Instance().MenuItemClickedSFX, "MenuItem");
    }

    public void OnMouseExitQuitButton()
    {
        QuitBackgroundImage.color = new Color(QuitBackgroundImage.color.r, QuitBackgroundImage.color.g, QuitBackgroundImage.color.b, 1.0f);
    }
}
