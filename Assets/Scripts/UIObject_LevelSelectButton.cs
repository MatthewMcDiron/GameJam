using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIObject_LevelSelectButton : MonoBehaviour
{
    [SerializeField] Image LevelBackgroundImage;
    [SerializeField] TextMeshProUGUI TimeCompletedText;
    [SerializeField] GameObject LevelPrefab;

    int LevelNumber = -1;
    GameObject LevelSelectCanvas;

    public void SetLevelSelectCanvasAndLevelNumber(GameObject _obj, int _levelNumber)
    {
        LevelSelectCanvas = _obj;
        LevelNumber = _levelNumber;
    }

    public void SetTimeCompletedText(LevelData _data)
    {
        string TimeText = _data.TimeHours + ":";

        if (_data.TimeMinutes < 10) { TimeText += "0"; }
        TimeText += _data.TimeMinutes + ":";

        if (_data.TimeSeconds < 10) { TimeText += "0"; }
        TimeText += _data.TimeSeconds;

        TimeCompletedText.text = TimeText;
    }

    public void OnMouseEnterButton()
    {
        LevelBackgroundImage.color = new Color(LevelBackgroundImage.color.r, LevelBackgroundImage.color.g, LevelBackgroundImage.color.b, 0.6f);
    }

    public void OnMouseExitButton()
    {
        LevelBackgroundImage.color = new Color(LevelBackgroundImage.color.r, LevelBackgroundImage.color.g, LevelBackgroundImage.color.b, 1.0f);
    }

    public void OnMouseClickButton()
    {
        if (LevelPrefab != null)
        {
            LevelManager.Instance().SetCurrentLevelNumber(LevelNumber);
            Instantiate(LevelPrefab, null);
            Destroy(LevelSelectCanvas);
        }
        else
        { Debug.Log("Level has not been added"); }
    }
}
