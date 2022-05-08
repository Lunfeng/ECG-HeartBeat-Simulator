using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECGManager
{
    private HeartControl heart;
    private PathControl path;
    public WaveControl wave1;
    public WaveControl wave2;
    public HeartPathData Monitor;

    public ECGManager()
    {
        heart = GameObject.Find("Heart").GetComponent<HeartControl>();
        path = GameObject.Find("ECGPath").GetComponent<PathControl>();
        wave1 = GameObject.Find("WavePage").GetComponent<WaveControl>();
        wave2 = GameObject.Find("WavePage2").GetComponent<WaveControl>();
        Monitor = GameObject.Find("MonitorApartPage").GetComponent<HeartPathData>();
    }

    public void SetPlaySpeed(float speed)
    {
        heart.SetPlaySpeed(speed);
        path.SetPathSpeed(speed);
    }

    public void SetPathSpeed(float speed)
    {
        path.SetPathSpeed(speed);
    }

    public void PauseaAnim()
    {
        heart.Pause();
    }
    public void StopAllPath()
    {
        path.Stop();
    }

    public void PlayDefaultPath()
    {
        StopAllPath();
        path.PlayDefault();
        path.SetPathSpeed(1);
    }

    public void RewindDefault()
    {
        path.RewindDefault();
    }

    public void ContinueAnim()
    {
        heart.Continue();
    }

    public void RestartAnim()
    {
        heart.Restart();
    }

    public void NextAnim()
    {
        GameManager.GetInstance().ecg.Monitor.NextAnim();
        int num = heart.NextAnim();
        path.StartPath(num);
        GameManager.GetInstance().ui.ChangeIntroductionPage(num);
    }

    public void PreviouAnim()
    {
        GameManager.GetInstance().ecg.Monitor.PreviousAnim();
        int num = heart.PreviousAnim();
        path.StartPath(num);
        GameManager.GetInstance().ui.ChangeIntroductionPage(num);
    }

}
