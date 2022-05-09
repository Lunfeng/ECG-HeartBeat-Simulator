using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECGIntroductionControl : MonoBehaviour
{
    public GameObject IntroductionPage;
    public List<CanvasRenderer> Pages;

    // Start is called before the first frame update
    void Start()
    {
        //Pages = IntroductionPage.transform.GetComponentsInChildren<CanvasRenderer>(true);
    }

    public void ApplyActiveToAll(bool isActive)
    {
        foreach(CanvasRenderer t in Pages)
        {
            t.gameObject.SetActive(isActive);
        }
    }

    public void Active(int i)
    {
        ApplyActiveToAll(false);
        Pages[i].gameObject.SetActive(true);
    }
}
