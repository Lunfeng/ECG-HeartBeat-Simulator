using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject Target;
    public GameObject m_Camera;
    public GameObject MonitorCamera;
    public GameObject WaveCamera;

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
    private bool canDrag = false;
    private RoatationFinishCallBack callback;

    public delegate void RoatationFinishCallBack();

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = UnityEngine.Camera.main.gameObject;
        Target = GameObject.Find("Target2");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit hit;
        //if (interactable && Input.GetKey(KeyCode.Mouse0))   //当用户可操作并且左键按下时
        //{
        //    if (Physics.Raycast(ray, out hit, int.MaxValue))
        //    {
        //        Debug.Log(hit.transform.gameObject.name);
        //        canDrag = true;
        //    }
        //    StopAllCoroutines();
        //    x = Input.GetAxis("Mouse X");                   //对鼠标坐标采样
        //    y = Input.GetAxis("Mouse Y");
        //    Spin(x, y);
        //}
        if (Physics.Raycast(ray, out hit, int.MaxValue))
        {
            if (interactable && Input.GetKeyDown(KeyCode.Mouse0) && hit.transform.name == "Heart")   //当用户可操作并且左键按下时
            {
                canDrag = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && canDrag)
        {
            StartCoroutine(InertialRotation(new Vector3(x, y, 0), Vector3.zero));
            canDrag = false;
        }
        if (canDrag)
        {
            StopAllCoroutines();
            x = Input.GetAxis("Mouse X");                   //对鼠标坐标采样
            y = Input.GetAxis("Mouse Y");
            Spin(x, y);
        }
        m_Camera.transform.LookAt(Target.transform);
        m_Camera.transform.position = Target.transform.position - m_Camera.transform.forward * camDistence;

        if (Input.GetKeyDown(KeyCode.Mouse1) && interactable)
        {
            AutoRotation(true);
        }
        if (Input.GetKeyDown(KeyCode.Mouse2) && interactable)
        {
            LookAtFront(Target.transform.eulerAngles, cb, false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        eulerAngle = m_Camera.transform.eulerAngles;
    }

    public void cb()
    {
        
    }

    public void Spin(float x, float y)
    {
        Vector3 targetAngle = (Vector3.right * -y + Vector3.up * x) * rotationSpeed + m_Camera.transform.eulerAngles;
        targetAngle.x = ClampAngle(targetAngle.x, -maxAngle, maxAngle);
        m_Camera.transform.eulerAngles = targetAngle;
        m_Camera.transform.position = Target.transform.position - m_Camera.transform.forward * camDistence;
    }

    public void LookAtFront(Vector3 targetPos, RoatationFinishCallBack callback, bool immediately)
    {
        targetPos.y += 90;
        this.callback = callback;
        //StartCoroutine(RotateTo(Camera.transform.eulerAngles, targetPos));
        if (immediately)
        {
            StopAllCoroutines();
            m_Camera.transform.eulerAngles = targetPos;
            m_Camera.transform.position = Target.transform.position - m_Camera.transform.forward * camDistence;
            //Debug.Log(targetPos);
            return;
        }
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
            Vector3 targetPos = m_Camera.transform.eulerAngles;
            targetPos.x = 0;
            //StartCoroutine(RotateTo(Camera.transform.eulerAngles, targetPos));
            StartCoroutine(RotateTo(targetPos));
            StartCoroutine(AutoRotation());
        }
        else
        {
            autoRotation = false;
        }
    }

    public void SwitchTarget(GameObject target)
    {
        Target = target;
    }

    public void EnableWaveCamera(bool on)
    {
        WaveCamera.SetActive(on);
        m_Camera.SetActive(!on);
    }

    public void EnableMonitorCamera(bool on)
    {
        MonitorCamera.SetActive(on);
    }

    public void EnableMonitor(bool enable)
    {
        if (enable)
        {
            m_Camera.GetComponent<Camera>().cullingMask = ~(1 << LayerMask.NameToLayer("PathSystem"));
        }
        else
        {
            m_Camera.GetComponent<Camera>().cullingMask = ~(1 << LayerMask.NameToLayer("Monitor") | 1 << LayerMask.NameToLayer("PathSystem"));
        }
    }

    IEnumerator RotateTo(Vector3 targetPos)
    {
        Vector3 offsetPos = targetPos - m_Camera.transform.eulerAngles;
        offsetPos.y = offsetPos.y > 180 ? offsetPos.y - 360 : offsetPos.y;
        offsetPos.x = offsetPos.x < -180 ? offsetPos.x + 360 : offsetPos.x;
        Vector3 pos = Vector3.zero;
        while ((offsetPos).magnitude > 0.1f)
        {
            pos.y = Mathf.Lerp(0, offsetPos.y, changeRate);
            pos.x = Mathf.Lerp(0, offsetPos.x, changeRate); 
            offsetPos.y -= pos.y;
            offsetPos.x -= pos.x;
            m_Camera.transform.eulerAngles += pos;
            m_Camera.transform.position = Target.transform.position - m_Camera.transform.forward * camDistence;
            yield return 0;
        }
        try
        {
            callback();
        }catch(Exception e)
        {
            Debug.Log(e.Message);
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
