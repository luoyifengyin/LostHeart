using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newboundary : MonoBehaviour {
    public GameObject LeftWall;
    public GameObject RightWall;
    public GameObject UpWall;
    public GameObject DownWall;
    float pos = 95f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pos -= 0.01f;
        if (FireworkScripts.GG != 1 && UIScript.isWin != 1)
       {
            LeftWall.transform.Translate(new Vector3(-0.01f, 0, 0));
            RightWall.transform.Translate(new Vector3(0.01f, 0, 0));
            UpWall.transform.Translate(new Vector3(-0.01f, 0, 0));
            DownWall.transform.Translate(new Vector3(0.01f, 0, 0));
       }
    }

    
}
