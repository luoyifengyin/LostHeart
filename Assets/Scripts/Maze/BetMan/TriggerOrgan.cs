using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOrgan : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            this.transform.position=new Vector3(this.transform.position.x,-1,this.transform.position.z);
        }
	}
}
