using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAIFour : MonoBehaviour
{
    public List<GameObject> AIFireworks;
    groundfireworks groundFireworksScript;
    int minFar = 0;
    int minRocketFar = 0;
    public int whichFar = 0;
    public float AISpeed = 0.0f;
    Vector3 target = new Vector3(0, 1.0f, 0);
    Quaternion AIQuaternion;
    float fireWorkRotate = 0;
    GameObject player;
    public float Idle = 1;
    public int accelerateTime = 180;
    PlayerMove Player;
    gameui uii;
    UIScript UUUUUUUI;
    Vector3 randomPoint;
    // Use this for initialization

    GameObject AI2;
    GameObject AI3;
    GameObject AI1;
    GameObject AI5;
    void Start()
    {
        groundFireworksScript = GameObject.Find("GroundFireworksSetting").GetComponent<groundfireworks>();
        AIQuaternion = this.transform.rotation;
        player = GameObject.Find("Player");
        AI2 = GameObject.Find("有害垃圾");
        AI3 = GameObject.Find("可回收垃圾");
        AI1 = GameObject.Find("垃圾分类");
        AI5 = GameObject.Find("干垃圾");
        Player = GameObject.Find("Player").GetComponent<PlayerMove>();
        randomPoint = new Vector3(Random.Range(-95f, 95f), 0.5f, Random.Range(-95f, 95f));
        uii = GameObject.Find("Canvas").GetComponent<gameui>();
        UUUUUUUI = GameObject.Find("Canvas").GetComponent<UIScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Seek();//寻找最近烟花
        moveAll();//所有AI拥有的烟花移动
        move();//AI的行动
        accelerate();//加速
        uii.Score(this.gameObject, AIFireworks.Count);
    }

    void Seek()
    {
        float far = 100.0f;
        float rocketFar = 100.0f;
        if (groundFireworksScript.GroundALLFireworks.Count > 0)
        {
            for (int i = 0; i < groundFireworksScript.GroundALLFireworks.Count; i++)
            {
                if (Vector3.Distance(this.transform.position, groundFireworksScript.GroundALLFireworks[i].transform.position) < far)
                {
                    far = Vector3.Distance(this.transform.position, groundFireworksScript.GroundALLFireworks[i].transform.position);
                    minFar = i;
                }
            }
        }
        if (groundFireworksScript.GroundALLRocket.Count > 0)
        {
            for (int i = 0; i < groundFireworksScript.GroundALLRocket.Count; i++)
            {
                if (Vector3.Distance(this.transform.position, groundFireworksScript.GroundALLRocket[i].transform.position) < rocketFar)
                {
                    rocketFar = Vector3.Distance(this.transform.position, groundFireworksScript.GroundALLRocket[i].transform.position);
                    minRocketFar = i;
                }
            }
        }

        if (far > rocketFar)
        {
            whichFar = 1;
        }
        else
        {
            whichFar = 0;
        }


    }

    void moveAll()
    {
        if (fireWorkRotate <= Mathf.PI * 2)
        {
            fireWorkRotate += 6.0f / 360.0f * Mathf.PI * 2;
        }
        else if (fireWorkRotate > Mathf.PI * 2)
        {
            fireWorkRotate = 0;
        }

        if (AIFireworks.Count > 0)
        {
            for (int i = 0; i < AIFireworks.Count; i++)
            {
                //Debug.Log("asd");
                float angle = fireWorkRotate + Mathf.PI * 2 * (float)i / (float)AIFireworks.Count;
                AIFireworks[i].transform.localPosition = Vector3.MoveTowards(AIFireworks[i].transform.localPosition, new Vector3(this.transform.position.x + 3f * Mathf.Sin(angle), 0.5f, this.transform.position.z + 3f * Mathf.Cos(angle)), 1.0f);
                AIFireworks[i].transform.LookAt(this.transform.position);
                AIFireworks[i].transform.Rotate(new Vector3(0, 90, 0));
            }
        }
    }

    void move()
    {
        this.transform.LookAt(target);
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        if (AIFireworks.Count < Player.playerFireworks.Count && Vector3.Distance(this.transform.position, player.transform.position) < 12.0f)
        {
            this.transform.rotation = Quaternion.Euler(-90, 0, 0);
            Idle = 0;
        }
        else
        {
            Idle = 1;
        }


        if (groundFireworksScript.GroundALLFireworks.Count <= 0)
        {
            target = randomPoint;
        }
        if (Vector3.Distance(this.transform.position, randomPoint) < 1.0f)
        {
            randomPoint = new Vector3(Random.Range(-95f, 95f), 0.5f, Random.Range(-95f, 95f));
        }

        if (AIFireworks.Count > Player.playerFireworks.Count && Idle == 1)
        {
            target = new Vector3(player.transform.position.x, 0.5f, player.transform.position.z);
        }

        if (groundFireworksScript.GroundALLFireworks.Count > 0 && AIFireworks.Count <= Player.playerFireworks.Count && Idle == 1 && whichFar == 0)
        {
            target = new Vector3(groundFireworksScript.GroundALLFireworks[minFar].transform.position.x, 0.5f, groundFireworksScript.GroundALLFireworks[minFar].transform.position.z);
        }
        else if (groundFireworksScript.GroundALLRocket.Count > 0 && AIFireworks.Count <= Player.playerFireworks.Count && Idle == 1 && whichFar == 1)
        {
            target = new Vector3(groundFireworksScript.GroundALLRocket[minRocketFar].transform.position.x, 0.5f, groundFireworksScript.GroundALLRocket[minRocketFar].transform.position.z);
        }
        if (Idle == 1)
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, new Vector3(target.x, 0.5f, target.z), AISpeed);
    }

    void accelerate()
    {
        if (accelerateTime < 30)
        {
            accelerateTime++;
            AISpeed = 0.3f;
        }
        else
        {
            AISpeed = 0.1f;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.transform.tag == "Fireworks")
        {
            groundFireworksScript.GroundALLFireworks.Remove(other.gameObject);
            other.transform.tag = "AIFireworks3";
            AIFireworks.Add(other.gameObject);
        }

        if (other.transform.tag == "Rocket")
        {
            groundFireworksScript.GroundALLRocket.Remove(other.gameObject);
            Destroy(other.gameObject);
            accelerateTime = 0;
        }


        if (other.collider.tag == "PlayerFireworks")
        {
            for (int i = 0; i < AIFireworks.Count; i++)
            {

                groundFireworksScript.GroundALLFireworks.Add(AIFireworks[i]);
                AIFireworks[i].tag = "Untagged";
                AIFireworks.Remove(AIFireworks[i]);

            }
            if (PlayerPrefs.HasKey("PlayerName"))
            {
                uii.killInformation(PlayerPrefs.GetString("PlayerName"), "湿垃圾");
            }
            Destroy(this.gameObject);
            uii.KillNum++;
            UUUUUUUI.PlayerKillNum++;
            Music.Kill.Play();
        }

        if (other.collider.tag == "AIFireworks1")
        {
            for (int i = 0; i < AIFireworks.Count; i++)
            {

                groundFireworksScript.GroundALLFireworks.Add(AIFireworks[i]);
                AIFireworks[i].tag = "Untagged";
                AIFireworks.Remove(AIFireworks[i]);

            }
            uii.killInformation("有害垃圾", "湿垃圾");
            Destroy(this.gameObject);
        }

        if (other.collider.tag == "AIFireworks2")
        {
            for (int i = 0; i < AIFireworks.Count; i++)
            {

                groundFireworksScript.GroundALLFireworks.Add(AIFireworks[i]);
                AIFireworks[i].tag = "Untagged";
                AIFireworks.Remove(AIFireworks[i]);

            }
            uii.killInformation("可回收垃圾", "湿垃圾");
            Destroy(this.gameObject);

        }

        if (other.collider.tag == "AIFireworks")
        {
            for (int i = 0; i < AIFireworks.Count; i++)
            {

                groundFireworksScript.GroundALLFireworks.Add(AIFireworks[i]);
                AIFireworks[i].tag = "Untagged";
                AIFireworks.Remove(AIFireworks[i]);

            }
            uii.killInformation("垃圾分类", "湿垃圾");
            Destroy(this.gameObject);

        }

        if (other.collider.tag == "AIFireworks4")
        {
            for (int i = 0; i < AIFireworks.Count; i++)
            {

                groundFireworksScript.GroundALLFireworks.Add(AIFireworks[i]);
                AIFireworks[i].tag = "Untagged";
                AIFireworks.Remove(AIFireworks[i]);

            }
            uii.killInformation("干垃圾", "湿垃圾");
            Destroy(this.gameObject);

        }
    }
}
