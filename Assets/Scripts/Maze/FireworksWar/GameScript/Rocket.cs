using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    gameui UUUIII;
	// Use this for initialization
	void Start () {
        UUUIII = GameObject.Find("Canvas").GetComponent<gameui>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionStay(Collision other)
    {
        if (other.collider.tag == "Wall")
        {
            this.transform.position = new Vector3(Random.Range(-95f * UUUIII.totalTime / 150f, 95f * UUUIII.totalTime / 150f), 0.5f, Random.Range(-95f * UUUIII.totalTime / 150f, 95f * UUUIII.totalTime / 150f));
        }
    }
}
