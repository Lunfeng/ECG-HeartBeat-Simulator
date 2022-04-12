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
    public GameObject Menu;
    public GameObject StartPage;
    public Button ShowPageButton;
    public Button ECGButton;
    public Button QuitButton;
    public Button ShowPageBackButton;
    public Button PauseButton;
    public Button ContinueButton;
    public Button ECGBackButton;
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

        //MenuButton
        ShowPageButton = Menu.transform.Find("ShowPageButton").GetComponent<Button>();
        ECGButton = Menu.transform.Find("ECGButton").GetComponent<Button>();
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

        ShowPageButton.onClick.AddListener(OnShowButtonClick);
        ECGButton.onClick.AddListener(OnECGButtonClick);
        QuitButton.onClick.AddListener(OnQuitButtonClick);
        ShowPageBackButton.onClick.AddListener(OnBackButtonClick);
        PauseButton.onClick.AddListener(OnPauseButtonClick);
        ContinueButton.onClick.AddListener(OnContinueButtonClick);
        NextAnimButton.onClick.AddListener(OnNextAnimButtonClick);
        PreviousAnimButton.onClick.AddListener(OnPreviousAnimButtonClick);
        PlayDefaultButton.onClick.AddListener(OnECGButtonClick);
        ECGBackButton.onClick.AddListener(OnBackButtonClick);
        ShowSpeedSlider.onValueChanged.AddListener(OnSpeedSliderValueChanged);
        ECGSpeedSlider.onValueChanged.AddListener(OnECGSpeedSliderValueChanged);
    }

    public void OnShowButtonClick()
    {
        game.SetStage(GameStage.ShowPage);
    }

    public void OnECGButtonClick()
    {
        ECGSpeedSlider.value = 0.4f;
        game.SetStage(GameStage.ECGPage);
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
