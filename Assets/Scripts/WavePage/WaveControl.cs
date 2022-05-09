using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using DG.Tweening;

public class WaveControl : MonoBehaviour
{
    //public static List<List<Pos>> pathDatas;
    public List<GameObject> StartPoints;
    public RectTransform ScaleObj;
    public float speed = 1;
    public float TrailWidth = 1;
    public float DotTrailWidth = 5;
    public Material TrailMaterial;
    public Material DotMaterial;
    public float PathRenderTime = 20;
    public float PathDuration = 4;
    public float DotPathDuration = 5;
    public float XFix = 1;
    public float YFix = 1;
    public int page = 0;
    public float AnimStartTime = 0;

    public string filename = "/ECG_Path.json";//_OverSpeed

    public Animation heartAnim;
    public AnimationState anim;

    public List<Wave> Waves;
    private float targetSpeed = 1;
    private bool isPause = false;

    public void SetAnimSpeed(float speed)
    {
        anim.speed = speed;
    }

    public void Start()
    {
        anim = heartAnim["play"];
        //SetAnimSpeed(2);
        ShowWaves();
    }

    public void SetSpeed()
    {
        switch (page)
        {
            case 0:
                targetSpeed = anim.length / (DotPathDuration / 5);
                SetAnimSpeed(targetSpeed);
                break;
            case 1:
                targetSpeed = anim.length / (DotPathDuration / 4);
                SetAnimSpeed(targetSpeed);
                break;
        }
    }

    public List<Wave> ShowWaves()
    {
        List<List<Pos>> pathDatas = ReadWaveData(filename);
        //List<List<Pos>> pathDatas = ReadWaveData("/ECG_Path_OverSpeed_OverSpeed.json");
        this.Waves = Waves = GenerateWaves(pathDatas, StartPoints, ScaleObj, DotPathDuration, XFix, YFix);
        return Waves;
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
            //t.OnStepComplete(() => {
            //    Debug.Log("Done");
            //    //t.Pause<Tween>();
            //    tr.emitting = false;
            //});

            GameObject Dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Dot.name = "Dot" + i;
            Dot.transform.SetParent(w.StartPoint.transform);
            Dot.transform.position = posArr[0];

            TrailRenderer tr2 = Dot.AddComponent<TrailRenderer>();
            tr2.emitting = true;
            tr2.time = 2;
            //tr2.startWidth = TrailWidth;
            tr2.startWidth = DotTrailWidth;
            tr2.endWidth = 0;
            tr2.material = DotMaterial;
            tr2.minVertexDistance = 0;

            Tween t2 = Dot.transform.DOPath(posArr.ToArray(), PlayDuration, PathType.CatmullRom).SetLoops(-1, LoopType.Yoyo).SetEase<Tween>(Ease.Linear);
            t2.OnStepComplete(() => {
                Debug.Log("Done2");
                tr2.emitting = false;
                t2.Pause<Tween>();
            });
            //t2.OnPause(() => {
            //    t2.GotoWaypoint(0, false);
            //});
            t2.OnPlay(() => {
                tr2.Clear();
                anim.time = AnimStartTime;
                tr2.emitting = true;
            });
            t2.Play<Tween>();

            w.DotTrail = tr2;
            w.TrailTween = t;
            w.DotTween = t2;
            Waves.Add(w);
        }
        anim.time = AnimStartTime;
        //StartCoroutine(DrawTrail(Waves));
        StartCoroutine(Restart(Waves, PlayDuration));
        return Waves;
    }

    public IEnumerator Restart(List<Wave> Waves, float playDuration)
    {
        Debug.Log(playDuration);
        yield return new WaitForSeconds(playDuration);
        Debug.Log(playDuration);
        while (true)
        {
            if (!isPause)
            {
                foreach (Wave w in Waves)
                {
                    w.DotTween.GotoWaypoint(0, false);
                }
                //yield return 0;
                foreach (Wave w in Waves)
                {
                    w.DotTween.Play<Tween>();
                    //w.DotTrail.emitting = true;
                }
                yield return new WaitForSeconds(playDuration);
            }
            else
            {
                yield return 0;
            }
            
        }
    }

    public void PlayAllDotTween()
    {
        StopAllCoroutines();
        foreach (Wave w in Waves)
        {
            w.DotTween.GotoWaypoint(0, false);
            w.DotTween.Play<Tween>();
        }
        StartCoroutine(Restart(Waves, DotPathDuration));
    }

    public void Pause()
    {
        foreach(Wave w in Waves)
        {
            w.DotTween.Pause<Tween>();
        }
        anim.speed = 0;
        isPause = true;
    }

    public void Continue()
    {
        foreach (Wave w in Waves)
        {
            w.DotTween.Play<Tween>();
        }
        anim.speed = targetSpeed;
        isPause = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public static List<List<Pos>> ReadWaveData(string filename)
    {
        List<List<Pos>> pathDatas = JsonMapper.ToObject<List<List<Pos>>>(File.ReadAllText(Application.streamingAssetsPath + filename));
        Debug.Log(pathDatas.Count);
        return pathDatas;
    }
}

public struct Wave
{
    public List<Pos> PosData;
    public Tween TrailTween;
    public Tween DotTween;
    public TrailRenderer DotTrail;
    public GameObject StartPoint;
}

public struct Pos
{
    public float X;
    public float Y;
}
