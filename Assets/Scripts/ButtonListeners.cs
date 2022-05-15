using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListeners : MonoBehaviour
{
    public GameManager game;
    public GameObject StartMenu;
    public GameObject ShowPage;
    public GameObject ECGPage;
    public GameObject WavePage;
    public GameObject WavePage2;
    public GameObject WavePage3;
    public GameObject WavePage4;
    public GameObject Menu;
    public GameObject StartPage;
    public Button ShowPageButton;
    public Button ECGButton;
    public Button WaveButton;
    public Button Wave2Button;
    public Button Wave3Button;
    public Button Wave4Button;
    public Button QuitButton;
    public Button ShowPageBackButton;
    public Button PauseButton;
    public Button ContinueButton;
    public Button ECGBackButton;
    public Button WaveBackButton;
    public Button Wave2BackButton;
    public Button Wave3BackButton;
    public Button Wave4BackButton;
    public Button NextAnimButton;
    public Button PreviousAnimButton;
    public Button PlayDefaultButton;
    public Slider ShowSpeedSlider;
    public Slider ECGSpeedSlider;

    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.GetInstance();
        StartMenu = gameObject.transform.GetChild(0).gameObject;
        Menu = StartMenu.transform.GetChild(0).gameObject;

        ShowPage = gameObject.transform.GetChild(1).gameObject;
        ECGPage = gameObject.transform.GetChild(2).gameObject;
        WavePage = GameObject.Find("WavePage");
        WavePage2 = GameObject.Find("WavePage2");
        WavePage3 = GameObject.Find("WavePage3");
        WavePage4 = GameObject.Find("WavePage4");

        //MenuButton
        ShowPageButton = Menu.transform.Find("ShowPageButton").GetComponent<Button>();
        ECGButton = Menu.transform.Find("ECGButton").GetComponent<Button>();
        WaveButton = Menu.transform.Find("WaveButton").GetComponent<Button>();
        Wave2Button = Menu.transform.Find("Wave2Button").GetComponent<Button>();
        Wave3Button = Menu.transform.Find("Wave3Button").GetComponent<Button>();
        Wave4Button = Menu.transform.Find("Wave4Button").GetComponent<Button>();
        QuitButton = Menu.transform.Find("QuitButton").GetComponent<Button>();
        
        //ShowPageButton
        ShowPageBackButton = ShowPage.transform.Find("Back").GetComponent<Button>();
        ShowSpeedSlider = ShowPage.transform.Find("SpeedSlider").GetComponent<Slider>();

        //ECGPageButton
        ECGSpeedSlider = ECGPage.transform.Find("SpeedSlider").GetComponent<Slider>();
        PauseButton = ECGPage.transform.Find("Pause").GetComponent<Button>();
        ContinueButton = ECGPage.transform.Find("Continue").GetComponent<Button>();
        NextAnimButton = ECGPage.transform.Find("NextAnim").GetComponent<Button>();
        PreviousAnimButton = ECGPage.transform.Find("PreviousAnim").GetComponent<Button>();
        PlayDefaultButton = ECGPage.transform.Find("PlayDefault").GetComponent<Button>();
        ECGBackButton = ECGPage.transform.Find("Back").GetComponent<Button>();
        WaveBackButton = WavePage.transform.Find("Back").GetComponent<Button>();
        Wave2BackButton = WavePage2.transform.Find("Back").GetComponent<Button>();
        Wave3BackButton = WavePage3.transform.Find("Back").GetComponent<Button>();
        Wave4BackButton = WavePage4.transform.Find("Back").GetComponent<Button>();
        
        ShowPageButton.onClick.AddListener(OnShowButtonClick);
        ECGButton.onClick.AddListener(OnECGButtonClick);
        WaveButton.onClick.AddListener(OnWaveButtonClick);
        Wave2Button.onClick.AddListener(OnWave2ButtonClick);
        Wave3Button.onClick.AddListener(OnWave3ButtonClick);
        Wave4Button.onClick.AddListener(OnWave4ButtonClick);
        QuitButton.onClick.AddListener(OnQuitButtonClick);
        ShowPageBackButton.onClick.AddListener(OnBackButtonClick);
        PauseButton.onClick.AddListener(OnPauseButtonClick);
        ContinueButton.onClick.AddListener(OnContinueButtonClick);
        NextAnimButton.onClick.AddListener(OnNextAnimButtonClick);
        PreviousAnimButton.onClick.AddListener(OnPreviousAnimButtonClick);
        PlayDefaultButton.onClick.AddListener(OnECGButtonClick);
        ECGBackButton.onClick.AddListener(OnBackButtonClick);
        WaveBackButton.onClick.AddListener(OnBackButtonClick);
        Wave2BackButton.onClick.AddListener(OnBackButtonClick);
        Wave3BackButton.onClick.AddListener(OnBackButtonClick);
        Wave4BackButton.onClick.AddListener(OnBackButtonClick);
        ShowSpeedSlider.onValueChanged.AddListener(OnSpeedSliderValueChanged);
        ECGSpeedSlider.onValueChanged.AddListener(OnECGSpeedSliderValueChanged);
    }

    public void OnShowButtonClick()
    {
        game.SetStage(GameStage.ShowPage);
    }

    public void OnECGButtonClick()
    {
        ECGSpeedSlider.value = 1f;
        game.SetStage(GameStage.ECGPage);
    }
    
    public void OnWaveButtonClick()
    {
        game.SetStage(GameStage.WavePage);
    }

    public void OnWave2ButtonClick()
    {
        game.SetStage(GameStage.WavePage2);
    }

    public void OnWave3ButtonClick()
    {
        game.SetStage(GameStage.WavePage3);
    }

    public void OnWave4ButtonClick()
    {
        game.SetStage(GameStage.WavePage4);
    }

    public void OnQuitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnBackButtonClick()
    {
        game.SetStage(GameStage.StartMenu);
    }

    public void OnSpeedSliderValueChanged(float value)
    {
        game.SetGameSpeed(value * 2f);
    }

    public void OnECGSpeedSliderValueChanged(float value)
    {
        game.SetGameSpeed(value * 0.1f);
    }

    public void OnPauseButtonClick()
    {
        game.ecg.PauseaAnim();
    }

    public void OnContinueButtonClick()
    {
        game.ecg.ContinueAnim();
    }

    public void OnNextAnimButtonClick()
    {
        game.ecg.NextAnim();
    }

    public void OnPreviousAnimButtonClick()
    {
        game.ecg.PreviouAnim();
    }
}
