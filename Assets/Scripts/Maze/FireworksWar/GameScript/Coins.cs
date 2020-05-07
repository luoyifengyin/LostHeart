using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {
    gameui UUUIII;
    groundfireworks groundFireworks;
	// Use this for initialization
	void Start () {
        UUUIII = GameObject.Find("Canvas").GetComponent<gameui>();
        groundFireworks = GameObject.Find("GroundFireworksSetting").GetComponent<groundfireworks>();
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
        if (other.collider.tag == "PlayerFireworks" || other.collider.tag == "Player")
        {
            UUUIII.PropCoinsNum += 5;
            Music.CoinsPropGet.Play();
            groundFireworks.GroundALLCoins.Remove(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
