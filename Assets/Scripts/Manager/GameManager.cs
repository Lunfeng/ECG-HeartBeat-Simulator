using UnityEngine;

public class GameManager
{
    private static GameManager Singleton = null;
    private GameStage stage;
    private CameraManager camera;
    private UIManager ui;
    private ECGManager ecg;

    public GameManager()
    {
        stage = GameStage.StartMenu;
        camera = new CameraManager();
        ui = new UIManager();
        ecg = new ECGManager();
    }

    public static GameManager GetInstance()
    {
        if (Singleton == null)
        {
            Singleton = new GameManager();
        }
        return Singleton;
    }

    public static void DestroyGameManager()
    {
        Singleton = null;
    }

    public void Init()
    {
        ui.InitUI();
        camera.InitCamera();
    }

    public void SetStage(GameStage stage)
    {
        this.stage = stage;
        switch (stage)
        {
            case GameStage.StartMenu:
                StartMenu();
                break;
            case GameStage.ShowPage:
                ShowPage();
                break;
            case GameStage.ECGPage:
                ECGPage();
                break;
        }
    }

    public GameStage GetStage()
    {
        return stage;
    }

    public void SetGameSpeed(float speed)
    {
        ecg.SetPlaySpeed(Mathf.Clamp(speed, 0f, 2f));
        ui.SetSpeedText(speed);
    }

    private void StartMenu()
    {
        Debug.Log("StartMenu");
        camera.ResetCamera(true);
        camera.SwitchToShowMode(false);
        ui.ResetPlaySpeed();
        ui.ActiveStartMenu();
    }

    private void ShowPage()
    {
        Debug.Log("ShowMenu");
        ui.ActiveShowPage();
        camera.SwitchToShowMode(true);
    }

    private void ECGPage()
    {
        Debug.Log("ECGMenu");
    }
}

public enum GameStage
{
    StartMenu,
    ShowPage,
    ECGPage
}
