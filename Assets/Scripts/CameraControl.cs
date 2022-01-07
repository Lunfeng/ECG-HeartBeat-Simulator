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
    public float rateOfDecay = 0.01f;

    [SerializeField]
    private float mouseVelocity = 0;

    private float x;
    private float y;

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
                StartCoroutine(InertialRotation(GetRotationVelocity()));
            Camera.transform.LookAt(Heart.transform);
            Camera.transform.position = Heart.transform.position - Camera.transform.forward * camDistence;
        }
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

    IEnumerator InertialRotation(Vector3 mousePosDelta)
    {
        Vector3 pos = mousePosDelta;
        Debug.Log(pos);
        while(pos.magnitude > 0)
        {
            pos.x = Mathf.Lerp(pos.x, 0, rateOfDecay);
            pos.y = Mathf.Lerp(pos.y, 0, rateOfDecay);
            Spin(pos.x, pos.y);
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
