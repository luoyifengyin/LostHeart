using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObstacle : MonoBehaviour {

    public GameObject Obstacle;//障碍物
    public GameObject Obstacle2;//十字架障碍物
    public Material M2;
    public Material M4;


    GameObject obj;
    float i = 0;


	// Use this for initialization
	void Start () {
		obj = GameObject.Instantiate(Obstacle);
        obj.transform.tag = "Obstacle";
        obj.transform.position = new Vector3(Random.Range(40f, 85f), 0.5f, Random.Range(-35,-90));
        obj.transform.eulerAngles = new Vector3(0,90,0);
        obj.GetComponent<MeshRenderer>().material = M2;

        obj = GameObject.Instantiate(Obstacle);
        obj.transform.tag = "Obstacle";
        obj.transform.position = new Vector3(Random.Range(40f, 85f), 0.5f, Random.Range(35, 90));
        obj.transform.eulerAngles = new Vector3(0, 0, 0);

        obj = GameObject.Instantiate(Obstacle);
        obj.transform.tag = "Obstacle";
        obj.transform.position = new Vector3(Random.Range(-40f, -85f), 0.5f, Random.Range(35, 90));
        obj.transform.eulerAngles = new Vector3(0, 90, 0);
        obj.GetComponent<MeshRenderer>().material = M2;

        obj = GameObject.Instantiate(Obstacle);
        obj.transform.tag = "Obstacle";
        obj.transform.position = new Vector3(Random.Range(-40f, -85f), 0.5f, Random.Range(-35, -90));
        obj.transform.eulerAngles = new Vector3(0, 0, 0);

        obj = GameObject.Instantiate(Obstacle2);
        obj.transform.tag = "Obstacle";
        obj.transform.GetChild(0).gameObject.tag = "Obstacle";
        obj.transform.GetChild(1).gameObject.tag = "Obstacle";
        obj.transform.position = new Vector3(Random.Range(-21f, 17f), 1f, Random.Range(-20, -80));
	}
	
	// Update is called once per frame
	void Update () {
        
        obj.transform.eulerAngles = new Vector3(0, i, 0);
        i += 1;
	}
}
