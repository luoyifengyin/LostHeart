using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killInf : MonoBehaviour
{
    float Speed = 20.0f;
    int DeleteTime = 0;
    // Use this for initialization
    void Start()
    {
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(-848f, 856f);
    }

    // Update is called once per frame
    void Update()
    {
        DeleteTime++;
        if (DeleteTime > 1000)
        {
            Destroy(this.gameObject);
        }
        this.GetComponent<RectTransform>().Translate(new Vector2(Speed, 0));
        if (this.GetComponent<RectTransform>().anchoredPosition.x > -50f)
        {
            Speed = 1f;
        }
        if (this.GetComponent<RectTransform>().anchoredPosition.x > 100f)
        {
            Speed = 20f;
        }
    }
}
