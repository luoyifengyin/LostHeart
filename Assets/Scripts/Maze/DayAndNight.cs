using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    float speed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        //this.InvokeRepeating("Round", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.right * speed);
    }

    //void Round()
    //{
    //    this.transform.Rotate(Vector3.right * speed);
    //}

}
