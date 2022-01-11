using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject Heart;
    public GameObject Camera;

    public bool interactable = true;
    public float camDistence = 5.0f;
    public float rotationSpeed = 5.0f;
    public float maxAngle = 60f;
    public float changeRate = 0.01f;
    public bool autoRotation = true;
    public float autoRotationSpeed = 0.2f;

    [SerializeField]
    Vector3 eulerAngle;

    private float x;
    private float y;
    private RoatationFinishCallBack callback;

    public delegate void RoatationFinishCallBack();

    // Start is called before the first frame update
    void Start()
    {
        Camera = UnityEngine.Camera.main.gameObject;
        Heart = GameObject.Find("Heart");
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable && Input.GetKey(KeyCode.Mouse0))   //当用户可操作并且左键按下时
        {
            StopAllCoroutines();
            x = Input.GetAxis("Mouse X");                   //对鼠标坐标采样
            y = Input.GetAxis("Mouse Y");
            Spin(x, y);
        }
        else
        {
            if(Input.GetKeyUp(KeyCode.Mouse0))
                StartCoroutine(InertialRotation(new Vector3(x, y, 0), Vector3.zero));
            Camera.transform.LookAt(Heart.transform);
            Camera.transform.position = Heart.transform.position - Camera.transform.forward * camDistence;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            AutoRotation(true);
            //LookAtFront(Heart.transform.eulerAngles, temp);
        }
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            LookAtFront(Heart.transform.eulerAngles, cb);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        eulerAngle = Camera.transform.eulerAngles;
    }

    public void cb()
    {

    }

    public void Spin(float x, float y)
    {
        Vector3 targetAngle = (Vector3.right * -y + Vector3.up * x) * rotationSpeed + Camera.transform.eulerAngles;
        targetAngle.x = ClampAngle(targetAngle.x, -maxAngle, maxAngle);
        Camera.transform.eulerAngles = targetAngle;
        Camera.transform.position = Heart.transform.position - Camera.transform.forward * camDistence;
    }

    public void LookAtFront(Vector3 targetPos, RoatationFinishCallBack callback)
    {
        targetPos.y += 90;
        this.callback = callback;
        //StartCoroutine(RotateTo(Camera.transform.eulerAngles, targetPos));
        StartCoroutine(RotateTo(targetPos));
    }

    public void UpdateDistence(float distence)
    {
        StartCoroutine(ChangeDistence(distence)); 
    }

    public void AutoRotation(bool isEnable)
    {
        StopAllCoroutines();
        if (isEnable)
        {
            autoRotation = true;
            Vector3 targetPos = Camera.transform.eulerAngles;
            targetPos.x = 0;
            //StartCoroutine(RotateTo(Camera.transform.eulerAngles, targetPos));
            StartCoroutine(RotateTo(targetPos));
            StartCoroutine(AutoRotation());
        }
        else
        {
            autoRotation = false;
            Debug.Log("no");
        }
    }

    //IEnumerator RotateTo(Vector3 targetPos)
    //{
    //    Vector3 offsetPos = Camera.transform.eulerAngles;
    //    while ((targetPos - offsetPos).magnitude > 0.1f)
    //    {
    //        if (Camera.transform.eulerAngles.x > 180)
    //        {
    //            offsetPos.x = 360 - Mathf.Lerp(360f - Camera.transform.eulerAngles.x, targetPos.x, changeRate);
    //        }
    //        else
    //        {
    //            offsetPos.x = Mathf.Lerp(Camera.transform.eulerAngles.x, targetPos.x, changeRate);
    //        }
    //        if (Mathf.Abs(Camera.transform.eulerAngles.y - targetPos.y) > 180)
    //        {
    //            offsetPos.y = 360 - Mathf.Lerp(360f - Camera.transform.eulerAngles.y, targetPos.y, changeRate);
    //        }
    //        else
    //        {
    //            offsetPos.y = Mathf.Lerp(Camera.transform.eulerAngles.y, targetPos.y, changeRate);
    //        }
    //        Camera.transform.eulerAngles = offsetPos;
    //        Camera.transform.position = Heart.transform.position - Camera.transform.forward * camDistence;
    //        yield return 0;
    //    }
    //    Debug.Log("Done");
    //    callback();
    //}

    IEnumerator RotateTo(Vector3 targetPos)
    {
        Vector3 offsetPos = targetPos - Camera.transform.eulerAngles;
        offsetPos.y = offsetPos.y > 180 ? offsetPos.y - 360 : offsetPos.y;
        offsetPos.x = offsetPos.x < -180 ? offsetPos.x + 360 : offsetPos.x;
        Vector3 pos = Vector3.zero;
        while ((offsetPos).magnitude > 0.1f)
        {
            pos.y = Mathf.Lerp(0, offsetPos.y, changeRate);
            pos.x = Mathf.Lerp(0, offsetPos.x, changeRate); 
            offsetPos.y -= pos.y;
            offsetPos.x -= pos.x;
            Camera.transform.eulerAngles += pos;
            Camera.transform.position = Heart.transform.position - Camera.transform.forward * camDistence;
            yield return 0;
        }
        try
        {
            callback();
        }catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    IEnumerator ChangeDistence(float distence)
    {
        while(Mathf.Abs(camDistence - distence) > 0)
        {
            camDistence = Mathf.Clamp(camDistence, distence, changeRate);
            yield return 0;
        }
        camDistence = distence;
    }

    IEnumerator InertialRotation(Vector3 currentPos, Vector3 targetPos)
    {
        while((currentPos - targetPos).magnitude > 0.01f)
        {
            currentPos.x = Mathf.Lerp(currentPos.x, targetPos.x, changeRate);
            currentPos.y = Mathf.Lerp(currentPos.y, targetPos.y, changeRate);
            //Debug.Log(currentPos);
            Spin(currentPos.x, currentPos.y);
            yield return 0;
        }
        Debug.Log("Done2");
    }

    IEnumerator AutoRotation()
    {
        yield return StartCoroutine(InertialRotation(Vector3.zero, Vector3.right * autoRotationSpeed));
        while (autoRotation)
        {
            Spin(autoRotationSpeed, 0);
            yield return 0;
        }
        yield return StartCoroutine(InertialRotation(Vector3.right * autoRotationSpeed, Vector3.zero));
    }

    public float ClampAngle(float value, float min, float max)
    {
        if(value < 180)
        {
            return Mathf.Clamp(value, min, max);
        }
        else
        {
            return Mathf.Clamp(value - 360, min, max);
        }
    }
}
