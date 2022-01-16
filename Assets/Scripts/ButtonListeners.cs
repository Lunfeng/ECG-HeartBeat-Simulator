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
    //public Button ShowPageResetButton;
    public Slider SpeedSlider;

    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.GetInstance();
        StartMenu = gameObject.transform.GetChild(0).gameObject;
        Menu = StartMenu.transform.GetChild(0).gameObject;

        ShowPage = gameObject.transform.GetChild(1).gameObject;
        ECGPage = gameObject.transform.GetChild(2).gameObject;

        ShowPageButton = Menu.transform.Find("ShowPageButton").GetComponent<Button>();
        ECGButton = Menu.transform.Find("ECGButton").GetComponent<Button>();
        QuitButton = Menu.transform.Find("QuitButton").GetComponent<Button>();
        ShowPageBackButton = ShowPage.transform.Find("Back").GetComponent<Button>();
        //ShowPageResetButton = ShowPage.transform.Find("Reset").GetComponent<Button>();
        SpeedSlider = ShowPage.transform.Find("SpeedSlider").GetComponent<Slider>();

        ShowPageButton.onClick.AddListener(OnShowButtonClick);
        ECGButton.onClick.AddListener(OnECGButtonClick);
        QuitButton.onClick.AddListener(OnQuitButtonClick);
        ShowPageBackButton.onClick.AddListener(OnBackButtonClick);
        //ShowPageResetButton.onClick.AddListener(OnBackButtonClick);
        SpeedSlider.onValueChanged.AddListener(OnSpeedSliderValueChanged);
    }

    public void OnShowButtonClick()
    {
        game.SetStage(GameStage.ShowPage);
    }

    public void OnECGButtonClick()
    {
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
}
