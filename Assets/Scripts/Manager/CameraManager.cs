using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    public CameraControl cameraControl;
    private GameObject Heart;
    private GameObject Target2;

    public CameraManager()
    {
        cameraControl = Camera.main.GetComponent<CameraControl>();
        Heart = GameObject.Find("Heart");
        Target2 = GameObject.Find("Target2");
    }

    public void InitCamera()
    {
        ResetCamera(true);
        SwitchToShowMode(false);
    }

    public void UndateUserInteraction(bool interactable)
    {
        cameraControl.interactable = interactable;
    }

    public void SetCameraDistence(float dis)
    {
        cameraControl.UpdateDistence(dis);
    }

    public void SetCameraSpeed(float speed)
    {
        cameraControl.rotationSpeed = speed;
    }

    public void ResetCamera(bool immediately)
    {
        UndateUserInteraction(false);
        cameraControl.LookAtFront(Heart.transform.eulerAngles, ResetFinished, immediately);
    }

    public void ResetFinished()
    {
        UndateUserInteraction(true);
    }

    public void SwitchToShowMode(bool Switch)
    {
        cameraControl.interactable = Switch;
        cameraControl.MonitorCamera.SetActive(!Switch);
        cameraControl.SwitchTarget(Switch ? Heart : Target2);
    }

    public void EnableMonitor(bool enable)
    {
        cameraControl.EnableMonitor(enable);
    }
}
