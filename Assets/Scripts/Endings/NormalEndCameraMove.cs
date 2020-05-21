using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEndCameraMove : MonoBehaviour
{
    int Speed=1;
    int Timee=0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Timee++;
        if (Timee <= 1200)
            Speed = 1;
        else
            Speed = -1;
        if (Timee > 2400)
        {
            Timee = 0;
        }
        this.transform.Rotate(Vector3.up * Speed * Time.deltaTime, Space.Self);
    }
}
