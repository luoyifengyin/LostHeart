using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkScripts : MonoBehaviour
{
    groundfireworks groundFireworks;
    PlayerMove Player;
    GameObject player;
    float flyTime = 0;
    float flyTimeTime = 0;
    public static int GG = 0;
    Vector3 startPoint;
    enemyAI AI1;
    enemyAITwo AI2;
    enemyAIThree AI3;
    enemyAIFour AI4;
    enemyAIFive AI5;
    gameui UUUIII;
    
    // Use this for initialization
    void Start()
    {
        GG = 0;
        Player = GameObject.Find("Player").GetComponent<PlayerMove>();
        player = GameObject.Find("Player");
        groundFireworks = GameObject.Find("GroundFireworksSetting").GetComponent<groundfireworks>();
        AI1 = GameObject.Find("垃圾分类").GetComponent<enemyAI>();
        AI2 = GameObject.Find("有害垃圾").GetComponent<enemyAITwo>();
        AI3 = GameObject.Find("可回收垃圾").GetComponent<enemyAIThree>();
        AI4 = GameObject.Find("湿垃圾").GetComponent<enemyAIFour>();
        AI5 = GameObject.Find("干垃圾").GetComponent<enemyAIFive>();
        UUUIII = GameObject.Find("Canvas").GetComponent<gameui>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.tag == "Untagged")
        {
            if (flyTime == 0)
            {
                startPoint = new Vector3(this.transform.position.x + Random.Range(-10f, 10f), 0.5f, this.transform.position.z + Random.Range(-10f, 10f));
                flyTime = 1;
            }
            this.transform.localPosition = Vector3.MoveTowards(this.transform.position, startPoint, 1f);
            flyTimeTime++;
            if (flyTimeTime < 60)
            {
                this.transform.tag = "Untagged";
            }
            if (flyTimeTime >= 60)
            {
                this.transform.tag = "Fireworks";
                flyTime = 0;
                flyTimeTime = 0;
            }
        }
        if (this.transform.tag == "PlayerFireworks")
        {
            transform.LookAt(Player.transform.position);
            if (Player.Idle == 0)
            {
                transform.Rotate(new Vector3(90, 0, 0));
            }
            if (Player.Idle == 1)
            {
                transform.Rotate(new Vector3(0, 90, 0));
            }
        }
    }

    void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void OnCollisionStay(Collision other)
    {
        if (this.transform.tag == "PlayerFireworks")
        {
            if (other.collider.tag == "Fireworks")
            {
                Player.playerFireworks.Add(other.gameObject);
                other.transform.tag = "PlayerFireworks";
                other.gameObject.layer = 10;
                groundFireworks.GroundALLFireworks.Remove(other.gameObject);
                Music.GetFireworks.Play();
            }

            if (other.collider.tag == "AIFireworks")
            {
                if ((Player.Idle == 0 && defense()) || Player.Idle == 1)
                {
                    Player.playerFireworks.Remove(this.gameObject);
                    this.transform.tag = "Untagged";
                    this.gameObject.layer = 8;
                    groundFireworks.GroundALLFireworks.Add(this.gameObject);
                    Music.Attack.Play();
                }
                if ((AI1.Idle == 0 && defense()) || AI1.Idle == 1)
                {

                    AI1.AIFireworks.Remove(other.gameObject);
                    other.transform.tag = "Untagged";
                    other.gameObject.layer = 8;
                    groundFireworks.GroundALLFireworks.Add(other.gameObject);
                    Music.Attack.Play();
                }
            }
            if (other.collider.tag == "AIFireworks1")
            {
                if ((Player.Idle == 0 && defense()) || Player.Idle == 1)
                {
                    Player.playerFireworks.Remove(this.gameObject);
                    this.transform.tag = "Untagged";
                    this.gameObject.layer = 8;
                    groundFireworks.GroundALLFireworks.Add(this.gameObject);
                    Music.Attack.Play();
                }
                if ((AI2.Idle == 0 && defense()) || AI2.Idle == 1)
                {
                    AI2.AIFireworks.Remove(other.gameObject);
                    other.transform.tag = "Untagged";
                    other.gameObject.layer = 8;
                    groundFireworks.GroundALLFireworks.Add(other.gameObject);
                    Music.Attack.Play();
                }
            }
            if (other.collider.tag == "AIFireworks2")
            {
                if ((Player.Idle == 0 && defense()) || Player.Idle == 1)
                {
                    Player.playerFireworks.Remove(this.gameObject);
                    this.transform.tag = "Untagged";
                    this.gameObject.layer = 8;
                    groundFireworks.GroundALLFireworks.Add(this.gameObject);
                    Music.Attack.Play();
                }
                if ((AI3.Idle == 0 && defense()) || AI3.Idle == 1)
                {
                    AI3.AIFireworks.Remove(other.gameObject);
                    other.transform.tag = "Untagged";
                    other.gameObject.layer = 8;
                    groundFireworks.GroundALLFireworks.Add(other.gameObject);
                    Music.Attack.Play();
                }
            }
            if (other.collider.tag == "AIFireworks3")
            {
                if ((Player.Idle == 0 && defense()) || Player.Idle == 1)
                {
                    Player.playerFireworks.Remove(this.gameObject);
                    this.transform.tag = "Untagged";
                    this.gameObject.layer = 8;
                    groundFireworks.GroundALLFireworks.Add(this.gameObject);
                    Music.Attack.Play();
                }
                if ((AI4.Idle == 0 && defense()) || AI4.Idle == 1)
                {
                    AI4.AIFireworks.Remove(other.gameObject);
                    other.transform.tag = "Untagged";
                    other.gameObject.layer = 8;
                    groundFireworks.GroundALLFireworks.Add(other.gameObject);
                    Music.Attack.Play();
                }
            }
            if (other.collider.tag == "AIFireworks4")
            {
                if ((Player.Idle == 0 && defense()) || Player.Idle == 1)
                {
                    Player.playerFireworks.Remove(this.gameObject);
                    this.transform.tag = "Untagged";
                    this.gameObject.layer = 8;
                    groundFireworks.GroundALLFireworks.Add(this.gameObject);
                    Music.Attack.Play();
                }
                if ((AI5.Idle == 0 && defense()) || AI5.Idle == 1)
                {
                    AI5.AIFireworks.Remove(other.gameObject);
                    other.transform.tag = "Untagged";
                    other.gameObject.layer = 8;
                    groundFireworks.GroundALLFireworks.Add(other.gameObject);
                    Music.Attack.Play();
                }
            }

            if (other.collider.tag == "Obstacle")
            {
                Player.playerFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
                Music.Attack.Play();
            }
            if (other.collider.tag == "Rocket")
            {
                groundFireworks.GroundALLRocket.Remove(other.gameObject);
                Destroy(other.gameObject);
                Player.accelerateTime = 0;
                Music.RocketPropGet.Play();
            }
        }


        if (this.transform.tag == "AIFireworks")
        {
            if (other.collider.tag == "Fireworks")
            {
                AI1.AIFireworks.Add(other.gameObject);
                other.transform.tag = "AIFireworks";
                other.gameObject.layer = 9;
                groundFireworks.GroundALLFireworks.Remove(other.gameObject);
            }
            if (other.collider.tag == "Obstacle")
            {
                AI1.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
            }
            if (other.collider.tag == "Player")
            {
               
                if (PlayerPrefs.HasKey("PlayerName") && GG != 1)
                {
                    UUUIII.killInformation("垃圾分类", PlayerPrefs.GetString("PlayerName"));
                }
                GG = 1;
            }
            if (other.collider.tag == "Rocket")
            {
                groundFireworks.GroundALLRocket.Remove(other.gameObject);
                Destroy(other.gameObject);
                AI1.accelerateTime = 0;
            }

            if (other.collider.tag == "AIFireworks1")
            {
                AI1.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
                AI2.AIFireworks.Remove(other.gameObject);
                other.transform.tag = "Untagged";
                other.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(other.gameObject);
            }
            if (other.collider.tag == "AIFireworks2")
            {
                AI1.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
                AI3.AIFireworks.Remove(other.gameObject);
                other.transform.tag = "Untagged";
                other.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(other.gameObject);
            }
            if (other.collider.tag == "AIFireworks3")
            {
                AI1.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
                AI4.AIFireworks.Remove(other.gameObject);
                other.transform.tag = "Untagged";
                other.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(other.gameObject);
            }
            if (other.collider.tag == "AIFireworks4")
            {
                AI1.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
                AI5.AIFireworks.Remove(other.gameObject);
                other.transform.tag = "Untagged";
                other.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(other.gameObject);
            }
        }
        if (this.transform.tag == "AIFireworks1")
        {
            if (other.collider.tag == "Fireworks")
            {
                AI2.AIFireworks.Add(other.gameObject);
                other.transform.tag = "AIFireworks1";
                other.gameObject.layer = 9;
                groundFireworks.GroundALLFireworks.Remove(other.gameObject);
            }
            if (other.collider.tag == "Obstacle")
            {
                AI2.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
            }
            if (other.collider.tag == "Player")
            {
                
                if (PlayerPrefs.HasKey("PlayerName")&& GG != 1)
                {
                    UUUIII.killInformation("有害垃圾", PlayerPrefs.GetString("PlayerName"));
                }
                GG = 1;
            }
            if (other.collider.tag == "Rocket")
            {
                groundFireworks.GroundALLRocket.Remove(other.gameObject);
                Destroy(other.gameObject);
                AI2.accelerateTime = 0;
            }

            if (other.collider.tag == "AIFireworks2")
            {
                AI2.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
                AI3.AIFireworks.Remove(other.gameObject);
                other.transform.tag = "Untagged";
                other.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(other.gameObject);
            }
            if (other.collider.tag == "AIFireworks3")
            {
                AI2.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
                AI4.AIFireworks.Remove(other.gameObject);
                other.transform.tag = "Untagged";
                other.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(other.gameObject);
            }
            if (other.collider.tag == "AIFireworks4")
            {
                AI2.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
                AI5.AIFireworks.Remove(other.gameObject);
                other.transform.tag = "Untagged";
                other.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(other.gameObject);
            }
        }
        if (this.transform.tag == "AIFireworks2")
        {
            if (other.collider.tag == "Fireworks")
            {
                AI3.AIFireworks.Add(other.gameObject);
                other.transform.tag = "AIFireworks2";
                other.gameObject.layer = 9;
                groundFireworks.GroundALLFireworks.Remove(other.gameObject);
            }
            if (other.collider.tag == "Obstacle")
            {
                AI3.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
            }
            if (other.collider.tag == "Player")
            {
                
                if (PlayerPrefs.HasKey("PlayerName") && GG != 1)
                {
                    UUUIII.killInformation("可回收垃圾", PlayerPrefs.GetString("PlayerName"));
                }
                GG = 1;
            }
            if (other.collider.tag == "Rocket")
            {
                groundFireworks.GroundALLRocket.Remove(other.gameObject);
                Destroy(other.gameObject);
                AI3.accelerateTime = 0;
            }

            if (other.collider.tag == "AIFireworks3")
            {
                AI3.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
                AI4.AIFireworks.Remove(other.gameObject);
                other.transform.tag = "Untagged";
                other.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(other.gameObject);
            }
            if (other.collider.tag == "AIFireworks4")
            {
                AI3.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
                AI5.AIFireworks.Remove(other.gameObject);
                other.transform.tag = "Untagged";
                other.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(other.gameObject);
            }
        }
        if (this.transform.tag == "AIFireworks3")
        {
            if (other.collider.tag == "Fireworks")
            {
                AI4.AIFireworks.Add(other.gameObject);
                other.transform.tag = "AIFireworks3";
                other.gameObject.layer = 9;
                groundFireworks.GroundALLFireworks.Remove(other.gameObject);
            }
            if (other.collider.tag == "Obstacle")
            {
                AI4.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
            }
            if (other.collider.tag == "Player")
            {
               
                if (PlayerPrefs.HasKey("PlayerName") && GG != 1)
                {
                    UUUIII.killInformation("湿垃圾", PlayerPrefs.GetString("PlayerName"));
                }
                GG = 1;
            }
            if (other.collider.tag == "Rocket")
            {
                groundFireworks.GroundALLRocket.Remove(other.gameObject);
                Destroy(other.gameObject);
                AI4.accelerateTime = 0;
            }

            if (other.collider.tag == "AIFireworks4")
            {
                AI4.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
                AI5.AIFireworks.Remove(other.gameObject);
                other.transform.tag = "Untagged";
                other.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(other.gameObject);
            }
        }
        if (this.transform.tag == "AIFireworks4")
        {
            if (other.collider.tag == "Fireworks")
            {
                AI5.AIFireworks.Add(other.gameObject);
                other.transform.tag = "AIFireworks4";
                other.gameObject.layer = 9;
                groundFireworks.GroundALLFireworks.Remove(other.gameObject);
            }
            if (other.collider.tag == "Obstacle")
            {
                AI5.AIFireworks.Remove(this.gameObject);
                this.transform.tag = "Untagged";
                this.gameObject.layer = 8;
                groundFireworks.GroundALLFireworks.Add(this.gameObject);
            }
            if (other.collider.tag == "Player")
            {
                
                if (PlayerPrefs.HasKey("PlayerName")&&GG != 1)
                {
                    UUUIII.killInformation("干垃圾", PlayerPrefs.GetString("PlayerName"));
                }
                GG = 1;
            }
            if (other.collider.tag == "Rocket")
            {
                groundFireworks.GroundALLRocket.Remove(other.gameObject);
                Destroy(other.gameObject);
                AI5.accelerateTime = 0;
            }
        }

        if (this.transform.tag == "Fireworks")
        {
            if (other.collider.tag == "Obstacle")
            {
                this.transform.position = new Vector3(other.transform.position.x + 5f, 0.5f, other.transform.position.z + 5f);
            }
            if (other.collider.tag == "Wall")
            {
                this.transform.position = new Vector3(Random.Range(-95f * UUUIII.totalTime / 150f, 95f * UUUIII.totalTime / 150f), 0.5f, Random.Range(-95f * UUUIII.totalTime / 150f, 95f * UUUIII.totalTime / 150f));
            }
        }
    }

    bool defense()
    {
        float i;
        i = Random.Range(0, 1.0f);

        if (i > 0.7f)
        {

            return false;
        }
        else
            return true;
    }
}
