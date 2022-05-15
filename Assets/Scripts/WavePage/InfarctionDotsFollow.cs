using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfarctionDotsFollow : MonoBehaviour
{
    public GameObject TargetObj;
    public Light PotLight;
    public float FlashSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlashTheLight());
        //PotLight.DOIntensity(0, 45).SetEase(Ease.Flash).Play();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = TargetObj.transform.position;
    }

    IEnumerator FlashTheLight()
    {
        float time = 0;
        while (true)
        {
            if(LayerMask.LayerToName(gameObject.layer) == "UI")
            {
                PotLight.intensity = Mathf.Abs(Mathf.Sin(time * FlashSpeed) * 45);
                time += Time.deltaTime;
            }
            else
            {
                PotLight.intensity = 0;
            }
            yield return 0;
        }
    }
}
