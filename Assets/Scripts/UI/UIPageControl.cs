using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPageControl : MonoBehaviour
{
    public GameObject UI;
    public GameObject startMenu;
    public GameObject showPage;
    public GameObject ECGPage;

    void Start()
    {
        UI = gameObject;
        startMenu = UI.transform.GetChild(0).gameObject;
        showPage = UI.transform.GetChild(1).gameObject;
        ECGPage = UI.transform.GetChild(2).gameObject;
    }

    public void UpdateUIPageActive(GameStage stage, bool activity)
    {
        foreach (GameStage s in Enum.GetValues(typeof(GameStage)))
        {
            if(stage == s)
            {
                SetUIActive(s, activity);
            }
            else
            {
                SetUIActive(s, !activity);
            }
        }
    }

    private void SetUIActive(GameStage stage, bool isActive)
    {
        switch (stage)
        {
            case GameStage.StartMenu:
                startMenu.SetActive(isActive);
                break;
            case GameStage.ShowPage:
                showPage.SetActive(isActive);
                break;
            case GameStage.ECGPage:
                ECGPage.SetActive(isActive);
                break;
        }
    }
}
