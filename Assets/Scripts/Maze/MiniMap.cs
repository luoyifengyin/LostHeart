using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    //public Transform target;
    private GameObject T;

     private void LateUpdate()
        {
            T = GameObject.FindGameObjectWithTag("Player");
            transform.position = new Vector3(T.transform.position.x, transform.position.y, T.transform.position.z);
            transform.rotation = Quaternion.Euler(new Vector3(90, T.transform.eulerAngles.y, 0)); 
        }
}
