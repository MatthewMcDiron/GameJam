using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintLevel : MonoBehaviour
{

    private static PaintLevel instance;

    [SerializeField] GameObject PlayerObject;
    [SerializeField] GameObject LevelUIPrefab;
    [SerializeField] PaintCanvas LevelPaintCanvas;
    UICanvas_Level LevelUI;
    [SerializeField] GameObject MainMenuPrefab;
    [SerializeField] float PercentageRateNeededForCompletion = 100;

    float TimeElapsedDetlaTimeSeconds = 0;
    bool LevelPaused = false;
    bool LevelComplete = false;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    public static PaintLevel Instance()
    {
        return instance;
    }

    private void Start()
    {
        GameObject LvCanvasobj = Instantiate(LevelUIPrefab);
        LevelUI = LvCanvasobj.GetComponent<UICanvas_Level>();

        AudioManager.Instance().PlayMusic(AudioManager.Instance().LevelMusic);
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelComplete)
        {  
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (!LevelPaused) { PauseLevel(); }
                else { ResumeLevel(); }
            }
            if (!LevelPaused)
            {
                UpdateTimeElapsed();
                UpdatePaintedPercentage();
            }
        }

        if (LevelPaintCanvas.GetPercentageOfCanvasColoured() >= PercentageRateNeededForCompletion)
        {
            OnLevelComplete();
        }
    }

    private void UpdateTimeElapsed()
    {
        TimeElapsedDetlaTimeSeconds += Time.deltaTime;
        int hours = Mathf.FloorToInt(TimeElapsedDetlaTimeSeconds / (60 * 60));
        int minutes = Mathf.FloorToInt(TimeElapsedDetlaTimeSeconds / 60);
        int seconds = Mathf.RoundToInt(TimeElapsedDetlaTimeSeconds % 60);

        LevelUI.SetCurrentTimeText(hours, minutes, seconds);
    }

    private void UpdatePaintedPercentage()
    {
        LevelUI.SetPercentageOfAreaComplete(LevelPaintCanvas.GetPercentageOfCanvasColoured()/PercentageRateNeededForCompletion*100.0f);
    }

    public void PauseLevel()
    {
        LevelPaused = true;
        LevelUI.ShowPauseLevelUI();
    }

    public void ResumeLevel()
    {
        LevelPaused = false;
        LevelUI.HidePauseLevelUI();
    }

    public void QuitLevel()
    {
        Instantiate(MainMenuPrefab);
        Destroy(LevelUI.gameObject);
        Destroy(gameObject);
    }

    public void OnLevelComplete()
    {
        LevelComplete = true;
        int hours = Mathf.FloorToInt(TimeElapsedDetlaTimeSeconds / (60 * 60));
        int minutes = Mathf.FloorToInt(TimeElapsedDetlaTimeSeconds / 60);
        int seconds = Mathf.RoundToInt(TimeElapsedDetlaTimeSeconds % 60);

        LevelUI.ShowLevelCompleteBackground(LevelManager.Instance().GetCurrentLevel(), hours, minutes, seconds);
    }

    public bool IsLevelPaused() { return LevelPaused; }
    public bool IsLevelComplete() { return LevelComplete; }
}
