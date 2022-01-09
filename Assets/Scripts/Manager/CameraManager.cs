using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    public CameraControl cameraControl = Camera.main.GetComponent<CameraControl>();
    public void EnableUserInteraction()
    {
        cameraControl.interactable = true;
    }

    public void DisableUserInteraction()
    {
        cameraControl.interactable = false;
    }

    public void SetCameraDistence(float dis)
    {
        cameraControl.UpdateDistence(dis);
    }

    public void SetCameraSpeed(float speed)
    {
        cameraControl.rotationSpeed = speed;
    }

    public void ResetCamera()
    {
        DisableUserInteraction();
        cameraControl.LookAtFront(GameObject.Find("Heart").transform.eulerAngles, ResetFinished);
    }

    public void ResetFinished()
    {
        Debug.Log("Get it!");
        EnableUserInteraction();
    }
}
