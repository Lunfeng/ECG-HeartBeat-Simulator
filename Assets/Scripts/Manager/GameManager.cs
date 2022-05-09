using UnityEngine;

public class GameManager
{
    private static GameManager Singleton = null;
    private GameStage stage;
    public CameraManager camera;
    public UIManager ui;
    public ECGManager ecg;
    public HeartPathData pathData;

    public GameManager()
    {
        stage = GameStage.StartMenu;
        camera = new CameraManager();
        ui = new UIManager();
        ecg = new ECGManager();
        pathData = new HeartPathData();
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
        //ui.InitUI();
        //camera.InitCamera();
        //ecg.PlayDefaultPath();
        SetStage(GameStage.StartMenu);/////
        //StartMenu();
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
            case GameStage.WavePage:
                WavePage();
                break;
            case GameStage.WavePage2:
                WavePage2();
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
        camera.SwitchToShowMode(false);
        camera.ResetCamera(true);
        camera.EnableMonitor(true);
        ui.ResetPlaySpeed();
        ui.ActiveStartMenu();
        SetGameSpeed(1f);
        ecg.RestartAnim();
        ecg.RewindDefault();
        ecg.PlayDefaultPath();
        ecg.Monitor.DisableAllAnim();
        camera.cameraControl.EnableWaveCamera(false);
        camera.cameraControl.EnableMonitorCamera(true);
    }

    private void ShowPage()
    {
        Debug.Log("ShowMenu");
        ui.ActiveShowPage();
        camera.SwitchToShowMode(true);
        camera.EnableMonitor(false);
        camera.cameraControl.EnableWaveCamera(false);
        camera.cameraControl.EnableMonitorCamera(false);
    }

    private void ECGPage()
    {
        Debug.Log("ECGMenu");
        ui.ActiveECGPage();
        camera.SwitchToShowMode(false);
        camera.EnableMonitor(true);
        SetGameSpeed(0.05f);
        ecg.RestartAnim();
        ecg.RewindDefault();
        ecg.PlayDefaultPath();
        ecg.SetPathSpeed(0.04f);
        ecg.Monitor.DisableAllAnim();
        camera.cameraControl.EnableWaveCamera(false);
        camera.cameraControl.EnableMonitorCamera(true);
    }

    private void WavePage()
    {
        Debug.Log("WavePage");
        ui.ActiveWavePage();
        ecg.wave1.SetSpeed();
        camera.cameraControl.EnableWaveCamera(true);
        camera.cameraControl.EnableMonitorCamera(false);
    }

    private void WavePage2()
    {
        Debug.Log("WavePage");
        ui.ActiveWavePage2();
        ecg.wave2.SetSpeed();
        camera.cameraControl.EnableWaveCamera(true);
        camera.cameraControl.EnableMonitorCamera(false);
    }
}

public enum GameStage
{
    StartMenu,
    ShowPage,
    ECGPage,
    WavePage,
    WavePage2
}
