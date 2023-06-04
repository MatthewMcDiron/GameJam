using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICanvas_Level : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LevelCurrentTimeText;
    [SerializeField] TextMeshProUGUI LevelRecordTimeText;
    [SerializeField] TextMeshProUGUI PecentageOfAreaComplete;
    [SerializeField] GameObject PauseScreenBackground;

    [SerializeField] GameObject LevelCompleteBackground;
    [SerializeField] TextMeshProUGUI LevelCompleteNewRecordText;
    [SerializeField] TextMeshProUGUI LevelCompleteNewRecordTimeText;


    [SerializeField] Image ResumeButtonImage;
    [SerializeField] Image QuitButtonImage;
    [SerializeField] Image LevelFinishedButtonImage;

    // Start is called before the first frame update
    void Start()
    {
        PauseScreenBackground.SetActive(false);   
        LevelCompleteBackground.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetCurrentTimeText(int _hours, int _minutes, int _seconds)
    {
        string TimeText = _hours + ":";

        if (_minutes < 10) { TimeText += "0"; }
        TimeText += _minutes + ":";

        if (_seconds < 10) { TimeText += "0"; }
        TimeText += _seconds;

        LevelCurrentTimeText.text = TimeText;
    }

    public void SetRecordTimeText(LevelData _data)
    {
        LevelRecordTimeText.text = _data.TimeHours + ":" + _data.TimeMinutes + ":" + _data.TimeSeconds;
    }

    public void SetPercentageOfAreaComplete(float _percentage)
    {
        _percentage = (Mathf.RoundToInt(_percentage * 100) / 100.0f);
        if (_percentage > 100.0f) { _percentage = 100.0f; }
        PecentageOfAreaComplete.text = _percentage + "%";
    }

    public void ShowPauseLevelUI()
    {
        PauseScreenBackground.SetActive(true);
    }

    public void HidePauseLevelUI()
    {
        OnResumeButtonExit();
        OnQuitButtonExit();
        PauseScreenBackground.SetActive(false);
    }

    public void ShowLevelCompleteBackground(LevelData _data, int _hours, int _minutes, int _seconds)
    {
        LevelCompleteBackground.SetActive(true);

        bool NewRecord = false;
        if (!_data.Completed)
        {
            _data.Completed = true;

            _data.TimeHours = _hours;
            _data.TimeMinutes = _minutes;
            _data.TimeSeconds = _seconds;
            NewRecord = true;
        }
        else
        {
            if (_data.TimeHours > _hours)
            {
                if (_data.TimeMinutes > _minutes)
                {
                    if (_data.TimeMinutes > _seconds)
                    {
                        _data.TimeHours = _hours;
                        _data.TimeMinutes = _minutes;
                        _data.TimeSeconds = _seconds;
                        NewRecord = true;
                    }
                }
            }
        }

        if (NewRecord) 
        {
            LevelCompleteNewRecordText.gameObject.SetActive(true);
            LevelCompleteNewRecordTimeText.gameObject.SetActive(true);
            LevelCompleteNewRecordTimeText.text = LevelCurrentTimeText.text; 
        }
        else 
        {
            LevelCompleteNewRecordText.gameObject.SetActive(false);
            LevelCompleteNewRecordTimeText.gameObject.SetActive(false); 
        }

    }

    public void OnResumeButtonOver()
    {
        AudioManager.Instance().PlaySFX(AudioManager.Instance().MenuItemEnterSFX, "MenuItem");
        ResumeButtonImage.color = new Color(ResumeButtonImage.color.r, ResumeButtonImage.color.g, ResumeButtonImage.color.b, 0.6f);
    }

    public void OnQuitButtonOver()
    {
        AudioManager.Instance().PlaySFX(AudioManager.Instance().MenuItemEnterSFX, "MenuItem");
        QuitButtonImage.color = new Color(QuitButtonImage.color.r, QuitButtonImage.color.g, QuitButtonImage.color.b, 0.6f);
    }

    public void OnResumeButtonExit()
    {
        ResumeButtonImage.color = new Color(ResumeButtonImage.color.r, ResumeButtonImage.color.g, ResumeButtonImage.color.b, 1.0f);
    }

    public void OnQuitButtonExit()
    {
        QuitButtonImage.color = new Color(QuitButtonImage.color.r, QuitButtonImage.color.g, QuitButtonImage.color.b, 1.0f);
    }

    public void OnResumeButtonClicked()
    {
        AudioManager.Instance().PlaySFX(AudioManager.Instance().MenuItemClickedSFX, "MenuItem");
        PaintLevel.Instance().ResumeLevel();
    }

    public void OnLevelFinishedButtonOver()
    {
        AudioManager.Instance().PlaySFX(AudioManager.Instance().MenuItemEnterSFX, "MenuItem");
        LevelFinishedButtonImage.color = new Color(LevelFinishedButtonImage.color.r, LevelFinishedButtonImage.color.g, LevelFinishedButtonImage.color.b, 0.6f);
    }

    public void OnLevelFinishedButtonExit()
    {
        LevelFinishedButtonImage.color = new Color(LevelFinishedButtonImage.color.r, LevelFinishedButtonImage.color.g, LevelFinishedButtonImage.color.b, 1.0f);
    }

    public void OnQuitButtonClicked()
    {
        AudioManager.Instance().PlaySFX(AudioManager.Instance().MenuItemClickedSFX, "MenuItem");
        PaintLevel.Instance().QuitLevel();
    }

}
