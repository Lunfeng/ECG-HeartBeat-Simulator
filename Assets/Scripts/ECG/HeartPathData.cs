using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class HeartPathData
{
    public static List<AnimationTime> animList;
    public static AnimationTime DefaultPathAnimation;

    private Transform[] PathSets = GameObject.Find("ECGPath").transform.GetComponentsInChildren<Transform>();
    private Vector3[] PosSets;
    private Transform target;
    private AnimationTime P, PQ, QRS, ST, T;

    public HeartPathData()
    {
        target = GameObject.Find("PathTarget").transform;
        PosSets = new Vector3[PathSets.Length - 1];
        for (int i = 0; i < PathSets.Length - 1; i++)
        {
            PosSets[i] = PathSets[i + 1].position;
        }
        animList = new List<AnimationTime>();
        DefaultPathAnimation = new AnimationTime(0.00f, 0.40f, GetRange(PosSets, 1, 13), PathType.Linear, target);
        P = new AnimationTime(0.00f, 0.05f, GetRange(PosSets, 1, 4), PathType.Linear, target);
        PQ = new AnimationTime(0.05f, 0.10f, GetRange(PosSets, 4, 5), PathType.Linear, target);
        QRS = new AnimationTime(0.10f, 0.24f, GetRange(PosSets, 5, 8), PathType.Linear, target);
        ST = new AnimationTime(0.24f, 0.30f, GetRange(PosSets, 8, 9), PathType.Linear, target);
        T = new AnimationTime(0.30f, 0.40f, GetRange(PosSets, 9, 13), PathType.Linear, target);
        animList.Add(P);
        animList.Add(PQ);
        animList.Add(QRS);
        animList.Add(ST);
        animList.Add(T);
    }

    public Vector3[] GetRange(Vector3[] arr, int begin, int end)
    {
        //Color color = new Color(begin * end, end, begin + end);
        Vector3[] temp = new Vector3[end - begin + 1];
        for(int i = begin - 1, j = 0; i <= end - 1; i++, j++)
        {
            temp[j] = arr[i];
        }
        return temp;
    }
}

public struct AnimationTime
{
    public readonly float StartTime;
    public readonly float EndTime;
    public readonly float Duration;
    public readonly Tween tween;

    public AnimationTime(float StartTime, float EndTime, Vector3[] posSets, PathType pathType, Transform target)
    {
        this.StartTime = StartTime;
        this.EndTime = EndTime;
        this.Duration = EndTime - StartTime;
        this.tween = target.DOPath(posSets, (EndTime - StartTime) * 40, pathType).SetLoops(-1).SetOptions(false).SetEase(Ease.Linear);
        this.tween.id = (EndTime - StartTime);
        //tween.OnWaypointChange((int i ) => { Debug.Log(i); });
        tween.OnStepComplete(OnCompelet);
        tween.Rewind();
        //tween.GotoWaypoint(0, false);
    }

    public void OnCompelet()
    {
        tween.GotoWaypoint(1, true);
    }
}
