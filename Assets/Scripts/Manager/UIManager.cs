using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public UIPageControl ui = GameObject.Find("UI").GetComponent<UIPageControl>();
    public StartMenuControl startMenu = GameObject.Find("StartMenu").GetComponent<StartMenuControl>();
    private ShowPageControl showPage = GameObject.Find("ShowPage").GetComponent<ShowPageControl>();

    public void InitUI()
    {
        ui.UpdateUIPageActive(GameStage.StartMenu, true);
        startMenu.EnableStartPage(true);
    }

    public void ActiveShowPage()
    {
        ui.UpdateUIPageActive(GameStage.ShowPage, true);
    }

    public void ActiveStartMenu()
    {
        ui.UpdateUIPageActive(GameStage.StartMenu, true);
    }

    public void SetSpeedText(float speed)
    {
        showPage.SetSpeedText(speed);
    }

    public void ResetPlaySpeed()
    {
        showPage.SetSliderValue(0.5f);
        SetSpeedText(1f);
    }
}
