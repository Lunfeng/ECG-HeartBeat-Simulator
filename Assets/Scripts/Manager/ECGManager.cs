using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECGManager
{
    public HeartControl heart;

    public ECGManager()
    {
        heart = GameObject.Find("Heart").GetComponent<HeartControl>();
    }

    public void SetPlaySpeed(float speed)
    {
        heart.SetPlaySpeed(speed);
    }
}
