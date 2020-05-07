using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killInf2 : MonoBehaviour {
    int DeleteTime = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        DeleteTime++;
        if (DeleteTime > 900)
        {
            Destroy(this.gameObject);
        }
	}
}
