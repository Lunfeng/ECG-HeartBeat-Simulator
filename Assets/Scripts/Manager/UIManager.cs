using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public UIPageControl ui = GameObject.Find("UI").GetComponent<UIPageControl>();
    private StartMenuControl startMenu = GameObject.Find("StartMenu").GetComponent<StartMenuControl>();
    private ShowPageControl showPage = GameObject.Find("ShowPage").GetComponent<ShowPageControl>();
    private ECGPageControl ECGPage = GameObject.Find("ECGPage").GetComponent<ECGPageControl>();
    private ECGIntroductionControl intro = GameObject.Find("Introduction").GetComponent<ECGIntroductionControl>();

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

    public void ActiveECGPage()
    {
        ui.UpdateUIPageActive(GameStage.ECGPage, true);
        intro.Active(5);
    }

    public void SetSpeedText(float speed)
    {
        showPage.SetSpeedText(speed);
        ECGPage.SetSpeedText(speed);
    }

    public void ResetPlaySpeed()
    {
        showPage.SetSliderValue(0.5f);
        SetSpeedText(1f);
    }

    public void ChangeIntroductionPage(int num)
    {
        intro.Active(num);
    }
}
