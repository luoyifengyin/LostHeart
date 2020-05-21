using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapArrow : MonoBehaviour
{
    private GameObject T;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        T = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, T.transform.eulerAngles.z));
    }
}
