using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject Heart;
    public GameObject Camera;

    public bool canRotation = true;
    public float camDistence = 5.0f;
    public float rotationSpeed = 5.0f;
    public float maxAngle = 60f;
    public float changeRate = 0.01f;

    [SerializeField]
    private float mouseVelocity = 0;

    private float x;
    private float y;
    [SerializeField]
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        Camera = UnityEngine.Camera.main.gameObject;
        Heart = GameObject.Find("Heart");
    }

    // Update is called once per frame
    void Update()
    {
        if (canRotation && Input.GetKey(KeyCode.Mouse0))
        {
            StopAllCoroutines();
            x = Input.GetAxis("Mouse X");
            y = Input.GetAxis("Mouse Y");
            Spin(x, y);
        }
        else
        {
            if(Input.GetKeyUp(KeyCode.Mouse0))
                StartCoroutine(InertialRotation(GetRotationVelocity(), Vector3.zero));
            Camera.transform.LookAt(Heart.transform);
            Camera.transform.position = Heart.transform.position - Camera.transform.forward * camDistence;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            LookAt(Heart.transform.eulerAngles);
        }
        pos = Camera.transform.eulerAngles;
    }

    public void Spin(float x, float y)
    {
        Vector3 targetAngle = (Vector3.right * -y + Vector3.up * x) * rotationSpeed + Camera.transform.eulerAngles;
        targetAngle.x = ClampAngle(targetAngle.x, -maxAngle, maxAngle);
        Camera.transform.eulerAngles = targetAngle;
        Camera.transform.position = Heart.transform.position - Camera.transform.forward * camDistence;
    }

    public Vector3 GetRotationVelocity()
    {
        Vector3 mousePosDelta = new Vector3(x, y, 0);
        mouseVelocity = mousePosDelta.magnitude;
        return mousePosDelta;
    }

    public void LookAt(Vector3 targetPos)
    {
        targetPos.y += 90;
        StartCoroutine(RotateTo(Camera.transform.eulerAngles, targetPos));
    }

    IEnumerator RotateTo(Vector3 currentPos, Vector3 targetPos)
    {
        while ((currentPos - targetPos).magnitude > 0.1f)
        {
            if (currentPos.x > 180)
            {
                currentPos.x = 360 - Mathf.Lerp(360f - currentPos.x, targetPos.x, changeRate);
            }
            else
            {
                currentPos.x = Mathf.Lerp(currentPos.x, targetPos.x, changeRate);
            }
            currentPos.y = Mathf.Lerp(currentPos.y, targetPos.y, changeRate);
            Camera.transform.eulerAngles = currentPos;
            Camera.transform.position = Heart.transform.position - Camera.transform.forward * camDistence;
            yield return 0;
        }
    }

    IEnumerator InertialRotation(Vector3 currentPos, Vector3 targetPos)
    {
        while(currentPos.magnitude > 0)
        {
            currentPos.x = Mathf.Lerp(currentPos.x, targetPos.x, changeRate);
            currentPos.y = Mathf.Lerp(currentPos.y, targetPos.y, changeRate);
            Spin(currentPos.x, currentPos.y);
            yield return 0;
        }
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
