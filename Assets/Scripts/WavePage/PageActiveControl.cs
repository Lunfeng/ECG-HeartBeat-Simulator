using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageActiveControl : MonoBehaviour
{
    Image[] images;
    Text[] texts;
    // Start is called before the first frame update
    void Start()
    {
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LayerMask.LayerToName(gameObject.layer) == "UI")
        {
            gameObject.SetActive(true);
            foreach(Image img in images)
            {
                Color c = img.color;
                c.a = 0.3921569f;
                img.color = c;
            }
            foreach (Text t in texts)
            {
                Color c = t.color;
                c.a = 1;
                t.color = c;
            }
        }
        else
        {
            foreach (Image img in images)
            {
                Color c = img.color;
                c.a = 0;
                img.color = c;
            }
            foreach (Text t in texts)
            {
                Color c = t.color;
                c.a = 0;
                t.color = c;
            }
        }
    }
}
