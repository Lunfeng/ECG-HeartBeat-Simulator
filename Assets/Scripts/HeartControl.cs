using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartControl : MonoBehaviour
{
    public GameObject Heart;
    public Animation anim;

    public float playSpeed = 1.0f;

    void Start()
    {
        Heart = gameObject;
        anim = Heart.GetComponent<Animation>();
    }

    public void SetPlaySpeed(float speed)
    {
        anim["play"].speed = speed;
        playSpeed = speed;
    }

    public void Play()
    {
        anim.Play();
    }

    public void Pause()
    {
        SetPlaySpeed(0);
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
        anim.Rewind();
    }

    void Update()
    {
        
    }
}
