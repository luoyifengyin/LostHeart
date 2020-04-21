﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 foward = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(foward, Camera.main.transform.up);
    }
}
