using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Control Script/Mouse Look")]
public class MouseRound : MonoBehaviour {


    float x, y;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            x = Input.GetAxis("Mouse X");
            y = Input.GetAxis("Mouse Y");
            if (x != 0)
            {
                // 左右旋转
                transform.RotateAround(this.transform.position, Vector3.up, x * 50 * Time.deltaTime);
            }
            if (y != 0)
            {
                // 上下旋转
                transform.RotateAround(this.transform.position, transform.right, -y * 50 * Time.deltaTime);
            }
        }
    }
}
