using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class HeartPathData : MonoBehaviour
{
    public static List<AnimationTime> animList;
    public static List<Tween> tweenList;
    public static AnimationTime DefaultPathAnimation;


    private Transform[] PathSets;
    private Vector3[] PosSets;
    private Transform target;
    private AnimationTime P, PQ, QRS, ST, T;

    public List<Wave> Waves;
    private WaveControl waveControl;
    public List<GameObject> StartPoints;
    public RectTransform ScaleObj;
    public float speed = 1;
    public float TrailWidth = 1;
    public Material TrailMaterial;
    public float PathRenderTime = 20;
    public float PathDuration = 4;
    public float DotPathDuration = 5;
    public float XFix = 1;
    public float YFix = 1;
    private int index = -1;

    public int NextAnim()
    {
        if(index < 4)
        {
            if(index > -1)
                Waves[index].TrailTween.Rewind();
            index++;
            foreach (Wave w in Waves)
            {
                w.DotTrail.Clear();
            }
            Waves[index].TrailTween.Play<Tween>();
            //for(int i = 0;i<Waves.Count;i++ )
            //{
            //    if(i == index)
            //    {
            //        Waves[index].TrailTween.Play<Tween>();
            //        index++;
            //        Waves[index].TrailTween.Play<Tween>();
            //    }
            //    else
            //    {

            //    }
            //    w.DotTrail.Clear();
            //}
        }
        return index;
    }

    public int PreviousAnim()
    {
        if (index > 0)
        {
            Waves[index].TrailTween.Rewind();
            index--;
            foreach (Wave w in Waves)
            {
                w.DotTrail.Clear();
            }
            Waves[index].TrailTween.Play<Tween>();
        }
        return index;
    }

    public void DisableAllAnim()
    {
        if (index == -1)
            return;
        Waves[index].TrailTween.Rewind();
        index = -1;
        foreach(Wave w in Waves)
        {
            w.DotTrail.Clear();
            w.TrailTween.Rewind();
        }
    }

    public void Start()
    {
        PathSets = GameObject.Find("ECGPath").transform.GetComponentsInChildren<Transform>();
        waveControl = GameObject.Find("MonitorPage").GetComponent<WaveControl>();
        List<GameObject> targets = new List<GameObject>();
        PosSets = new Vector3[PathSets.Length - 1];
        //PosSets = waveControl.ShowWaves();
        //for (int i = 0; i < 5; i++)
        //{
        //    targets.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        //    //PosSets[i] = PathSets[i + 1].position;
        //}


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
        QRS = new AnimationTime(0.30f, 0.50f, GetRange(PosSets, 5, 8), PathType.Linear, target);
        ST = new AnimationTime(0.40f, 0.60f, GetRange(PosSets, 8, 9), PathType.Linear, target);
        T = new AnimationTime(0.90f, 1.30f, GetRange(PosSets, 9, 13), PathType.Linear, target);
        animList.Add(P);
        animList.Add(PQ);
        animList.Add(QRS);
        animList.Add(ST);
        animList.Add(T);
        ShowWaves();
    }

    public HeartPathData()
    {
        
    }

    public void ShowWaves()
    {
        List<List<Pos>> pathDatas = WaveControl.ReadWaveData("/ECG_Path_Parts.json");
        Waves = GenerateWaves(pathDatas, StartPoints, ScaleObj, DotPathDuration, XFix, YFix);
    }

    public List<Wave> GenerateWaves(List<List<Pos>> pathDatas, List<GameObject> StartPoints, RectTransform ScaleObj, float PlayDuration, float XFix = 1, float YFix = 1)
    {
        List<Wave> Waves = new List<Wave>();
        float scale = ScaleObj.anchoredPosition.x / 25;
        for (int i = 0; i < pathDatas.Count; i++)
        {
            Wave w = new Wave();
            w.PosData = pathDatas[i];
            w.StartPoint = StartPoints[i];

            List<Vector3> posArr = new List<Vector3>();
            Transform startobj = StartPoints[i].transform;
            foreach (Pos p in w.PosData)
            {
                Vector3 pos = startobj.position + startobj.up * p.Y * scale * YFix + startobj.right * p.X * scale * XFix;
                posArr.Add(pos);
            }
            GameObject Target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Target.name = "Target" + i;
            Target.transform.SetParent(w.StartPoint.transform);
            Target.transform.position = posArr[0] + startobj.transform.forward * -1;

            TrailRenderer tr = Target.AddComponent<TrailRenderer>();
            tr.emitting = true;
            tr.time = PathRenderTime + PathDuration + 2;
            tr.startWidth = TrailWidth;
            tr.endWidth = TrailWidth;
            tr.material = TrailMaterial;
            tr.minVertexDistance = 0;

            Tween t = Target.transform.DOPath(posArr.ToArray(), PathDuration, PathType.CatmullRom).SetLoops(-1, LoopType.Yoyo).SetEase<Tween>(Ease.Linear);
            t.GotoWaypoint(0, false);
            
            w.TrailTween = t;
            w.DotTrail = tr;
            Waves.Add(w);
        }
        return Waves;
    }

    public Vector3[] GetRange(Vector3[] arr, int begin, int end)
    {
        //Color color = new Color(begin * end, end, begin + end);
        Vector3[] temp = new Vector3[end - begin + 1];
        for (int i = begin - 1, j = 0; i <= end - 1; i++, j++)
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

