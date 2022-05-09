using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuControl : MonoBehaviour
{
    public GameObject StartMenu;
    public GameObject StartPage;
    public Button ShowPageButton;
    public Button ECGButton;
    public Button QuitButton;

    private bool isStartPage = true;

    // Start is called before the first frame update
    void Start()
    {
        StartMenu = gameObject.transform.GetChild(0).gameObject;
        StartPage = gameObject.transform.GetChild(1).gameObject;
        EnableStartPage(true);
    }

    void Update()
    {
        if(isStartPage)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                EnableStartPage(false);
        }
    }

    public void EnableStartPage(bool enable)
    {
        StartMenu.SetActive(!enable);
        StartPage.SetActive(enable);
        isStartPage = enable;
    }
}
