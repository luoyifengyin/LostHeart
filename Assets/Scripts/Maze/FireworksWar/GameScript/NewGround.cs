using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGround : MonoBehaviour {

    public Material My1;
    public Material My2;
    public GameObject ground;
    int k;

	// Use this for initialization
	void Start () {
        k = Random.Range(0, 2); 
	}
	
	// Update is called once per frame
	void Update () {
        
        if (k == 0)
        {
            ground.GetComponent<MeshRenderer>().material = My1;
        }
        else
        {
            ground.GetComponent<MeshRenderer>().material = My2;
        }

	}
}
