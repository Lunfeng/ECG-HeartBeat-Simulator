using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathControl : MonoBehaviour
{
    public AnimationTime current;
    private float speed = DOTween.timeScale;


    public void ResetPath()
    {
        current = HeartPathData.animList[0];
    }

    public void SetPathSpeed(float speed)
    {
        //DOTween.timeScale = speed * 10;
        this.speed = DOTween.timeScale;
    }

    public void StartPath(int num)
    {
        if(!HeartPathData.animList[num].Equals(current))
        {
            current.tween.Pause<Tween>();
            current = HeartPathData.animList[num];
            current.tween.Play<Tween>();
            current.tween.GotoWaypoint(1, true);
        }
    }

    public void PlayDefault()
    {
        HeartPathData.DefaultPathAnimation.tween.Play<Tween>();
        HeartPathData.DefaultPathAnimation.tween.GotoWaypoint(1, true);
    }

    public void RewindDefault()
    {
        HeartPathData.DefaultPathAnimation.tween.Rewind();
    }

    public void Pause()
    {
        DOTween.timeScale = 0;
    }

    public void Continue()
    {
        DOTween.timeScale = speed;
    }

    public void Stop()
    {
        current.tween.Pause();
    }

    public void Restart()
    {

    }

    IEnumerator StartFrom(Tween tween)
    {
        yield return 0;
    }
}
