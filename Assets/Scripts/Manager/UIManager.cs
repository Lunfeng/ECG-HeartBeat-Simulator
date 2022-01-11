using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public UIPageControl UIControl = GameObject.Find("UI").GetComponent<UIPageControl>();
    private GameManager manager = GameManager.GetGameManager();

    public void UpdateUIPageActive(GameStage stage, bool activity)
    {
        UIControl.UpdateUIPageActive(stage, activity);
    }
}
