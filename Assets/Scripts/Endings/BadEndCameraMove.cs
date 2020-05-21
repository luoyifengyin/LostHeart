using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEndCameraMove : MonoBehaviour
{
    float TranslateSpeed = 0.1f;
    float TranslateSpeedTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TranslateSpeedTime += 0.1f;

        transform.Translate(Vector3.forward * TranslateSpeed);
        if (TranslateSpeedTime > 80.0f)
        {
            transform.Rotate(0, 180, 0);
            TranslateSpeedTime = 0.1f;
        }
    }
}
