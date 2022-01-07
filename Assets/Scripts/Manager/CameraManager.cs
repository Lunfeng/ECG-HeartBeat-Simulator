using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    public CameraControl cameraControl = Camera.main.GetComponent<CameraControl>();
    public void EnableCameraRotation()
    {
        cameraControl.canRotation = true;
    }

    public void DisableCameraRotation()
    {
        cameraControl.canRotation = false;
    }

    public void SetCameraDistence(float dis)
    {
        Mathf.Clamp(dis, 1, 10);
        cameraControl.camDistence = dis;
    }

    public void SetCameraSpeed(float speed)
    {
        Mathf.Clamp(speed, 1, 10);
        cameraControl.rotationSpeed = speed;
    }

    public static void ResetCamera()
    {

    }
}
