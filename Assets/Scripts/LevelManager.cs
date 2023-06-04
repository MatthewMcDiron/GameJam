using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    static LevelManager instance = null;
    Dictionary<int, LevelData> levels = new Dictionary<int, LevelData>();
    int CurrentLevelNumber = -1;

    public static LevelManager Instance()
    {
        return instance;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }

        CreateLevelsData();
    }

    private void CreateLevelsData()
    {
        for (int i = 0; i < 5; i++)
        {
            LevelData level = new LevelData(i + 1);
            levels[i + 1] = level;
        }
    }

    public LevelData GetLevelData(int _levelNo)
    {
        return levels[_levelNo];
    }

    public void SetCurrentLevelNumber(int _levelNo) { CurrentLevelNumber = _levelNo; }
    public void ResetCurrentLevelNumber() { CurrentLevelNumber = -1; }
    public LevelData GetCurrentLevel() { return levels[CurrentLevelNumber]; }
}

public class LevelData
{
    public int levelNumber;
    public int TimeHours;
    public int TimeMinutes;
    public int TimeSeconds;
    public bool Completed;

    public LevelData(int _level)
    {
        levelNumber = _level;
        TimeHours = 0;
        TimeMinutes = 0;
        TimeSeconds = 0;
        Completed = false;
    }
}
