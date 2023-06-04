using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas_LevelSelect : MonoBehaviour
{
    [SerializeField] List<UIObject_LevelSelectButton> LevelSelectButtons;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance().SetCurrentLevelNumber(-1);

        for (int i = 0; i< LevelSelectButtons.Count; i++)
        {
            LevelSelectButtons[i].SetTimeCompletedText(LevelManager.Instance().GetLevelData(i + 1));
            LevelSelectButtons[i].SetLevelSelectCanvasAndLevelNumber(gameObject, i + 1);
        }
    }
}
