using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager Singleton = null;
    private GameStage stage;

    public GameManager()
    {
        stage = GameStage.StartMenu;
    }

    public static GameManager GetGameManager()
    {
        if (Singleton == null)
        {
            Singleton = new GameManager();
        }
        return Singleton;
    }

    public void SetStage(GameStage stage)
    {
        this.stage = stage;
        switch (stage)
        {
            case GameStage.StartMenu:
                break;
            case GameStage.ShowPage:
                break;
            case GameStage.ECGPage:
                break;
        }
    }

    public GameStage GetStage()
    {
        return stage;
    }

    public void SetGameSpeed(float speed)
    {

    }

    public void StartMenu()
    {
        
    }

    public void ShowPage()
    {

    }

    public void ECGPage()
    {

    }
}

public enum GameStage
{
    StartMenu,
    ShowPage,
    ECGPage
}
