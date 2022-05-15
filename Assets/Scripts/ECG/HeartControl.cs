using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeartControl : MonoBehaviour
{
    public GameObject Heart;
    public Animation anim;
    public static float playSpeed = 1.0f;
    public AnimationTime currentTime;
    //public static List<AnimationTime> animList = new List<AnimationTime>();

    private AnimationState state;
    //private AnimationTime P, PQ, QRS, ST, T;

    void Start()
    {
        Heart = gameObject;
        anim = Heart.GetComponent<Animation>();
        state = anim["play"];
        //Debug.Log(state.length);
    }

    public void SetPlaySpeed(float speed)
    {
        state.speed = speed;
        playSpeed = speed;
    }

    public void Play()
    {
        anim.Play();
    }

    public void Pause()
    {
        state.speed = 0;
    }

    public void Continue()
    {
        SetPlaySpeed(playSpeed);
    }

    public void Stop()
    {
        anim.Stop();
    }

    public void Restart()
    {
        StopAllCoroutines();
        anim.Rewind();
    }

    public int NextAnim()
    {
        int num = HeartPathData.animList.FindIndex(a => a.EndTime == currentTime.EndTime);
        num = num + 1 < HeartPathData.animList.Count ? num + 1 : num;
        if (num < 0) num = 0;
        ChangeAnimTime(HeartPathData.animList[num]);
        return num;
    }

    public int PreviousAnim()
    {
        int num = HeartPathData.animList.FindIndex(a => a.EndTime == currentTime.EndTime);
        num = num - 1 >= 0 ? num - 1 : num;
        if (num < 0) num = 0;
        ChangeAnimTime(HeartPathData.animList[num]);
        return num;
    }

    public void ChangeAnimTime(AnimationTime time)
    {
        if (time.EndTime == currentTime.EndTime)
            return;
        StopAllCoroutines();
        currentTime = time;
        StartCoroutine(PlayCurrentTime(true));
    }

    IEnumerator PlayCurrentTime(bool loop)
    {
        do
        {
            state.time = currentTime.StartTime;
            while (state.time <= currentTime.EndTime)
            {
                yield return 0;
            }
        } while (loop);
    }

    //public void Update()
    //{

    //    Debug.Log(state.time);
    //}
}
