using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundfireworks : MonoBehaviour
{
    public List<GameObject> GroundALLFireworks;//所有地面烟花
    public GameObject GroundFireworks;//烟花预制体

    public List<GameObject> GroundALLRocket;//所有地面火箭
    public GameObject GroundRocket;//火箭预制体

    public List<GameObject> GroundALLCoins;//所有地面金币箱
    public GameObject GroundCoins;//金币箱预制体

    public List<GameObject> GroundALLEXP;//所有地面经验道具
    public GameObject GroundEXP;//经验道具预制体

    int RocketTime = 0;//火箭生成时间
    int CoinsTime = 0;//金币箱生成时间
    int EXPTime = 0;//经验道具生成时间
    //GameObject groundFireworksa;
	// Use this for initialization
	void Start () {
        GameObject t_groundFireworks;
        for (int i = 0; i < 150; i++)
        {
            t_groundFireworks = GameObject.Instantiate(GroundFireworks);
            t_groundFireworks.transform.position = new Vector3(Random.Range(-95.0f, 95.0f), 0.5f, Random.Range(-95.0f, 95.0f));
            t_groundFireworks.tag = "Fireworks";
            t_groundFireworks.gameObject.layer = 8;
            GroundALLFireworks.Add(t_groundFireworks);
        }
	}

    // Update is called once per frame
    void Update()
    {
        RocketTime++;
        if (RocketTime > 300)
        {
            RocketTime = 0;
            GameObject t_groundRocket;
            t_groundRocket = GameObject.Instantiate(GroundRocket);
            t_groundRocket.transform.position = new Vector3(Random.Range(-95.0f, 95.0f), 0.5f, Random.Range(-95.0f, 95.0f));
            t_groundRocket.tag = "Rocket";
            GroundALLRocket.Add(t_groundRocket);
        }

        CoinsTime++;
        if (CoinsTime > 300)
        {
            CoinsTime = 0;
            GameObject t_groundCoins;
            t_groundCoins = GameObject.Instantiate(GroundCoins);
            t_groundCoins.transform.position = new Vector3(Random.Range(-95.0f, 95.0f), 0.5f, Random.Range(-95.0f, 95.0f));
            t_groundCoins.tag = "Coins";
            GroundALLCoins.Add(t_groundCoins);
        }

        EXPTime++;
        if (EXPTime > 300)
        {
            EXPTime = 0;
            GameObject t_groundEXP;
            t_groundEXP = GameObject.Instantiate(GroundEXP);
            t_groundEXP.transform.position = new Vector3(Random.Range(-95.0f, 95.0f), 0.5f, Random.Range(-95.0f, 95.0f));
            t_groundEXP.tag = "EXP";
            GroundALLEXP.Add(t_groundEXP);
        }
	}
}
