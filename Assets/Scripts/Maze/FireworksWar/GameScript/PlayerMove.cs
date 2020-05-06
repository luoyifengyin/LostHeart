using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {
    private Ray ray;//定义射线
    private RaycastHit hit = new RaycastHit();
    public static float moveSpeed = 0.2f;//移动速度
    public float Idle = 0;//是否进入Idle状态
    public List<GameObject> playerFireworks;//玩家烟花数组
    public GameObject ParticleSystemBlue;
    public GameObject ParticleSystemRed;
    public GameObject ParticleSystemYellow;
    float i = 0;
    public float accelerateTime = 180;
    groundfireworks groundFireworks;
    gameui UII;
    //GameObject obj;
    //public GameObject Fireworks;//烟花预制体定义
    float fireworksRound = 0;
	// Use this for initialization
    Quaternion idleFace;


    public GameObject playerFace1;
    public GameObject playerFace2;
    public GameObject playerFace3;
    public GameObject playerFace4;

    public GameObject groundFireworkss;//烟花预制体
	void Start () {
        //Debug.Log(CharacterChoiceBtn.xuanzee);
        if (CharacterChoiceBtn.xuanzee == 1)
        {
            playerFace1.SetActive(true);
        }
        if (CharacterChoiceBtn.xuanzee == 2)
        {
            playerFace2.SetActive(true);
        }
        if (CharacterChoiceBtn.xuanzee == 3)
        {
            playerFace3.SetActive(true);
        }
        if (CharacterChoiceBtn.xuanzee == 4)
        {
            playerFace4.SetActive(true);
        }
        groundFireworks = GameObject.Find("GroundFireworksSetting").GetComponent<groundfireworks>();
        UII = GameObject.Find("Canvas").GetComponent<gameui>();
        accelerateTime = 180;
        idleFace = this.transform.rotation;

        if (SkipScene.isAd == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject t_groundFireworks;
                t_groundFireworks = GameObject.Instantiate(groundFireworkss);
                t_groundFireworks.transform.tag = "PlayerFireworks";
                t_groundFireworks.gameObject.layer = 10;
                playerFireworks.Add(t_groundFireworks.gameObject);
            }
            accelerateTime = 0;
        }
        //obj = GameObject.Instantiate(Fireworks);
        //playerFireworks.Add(obj);
        //obj.transform.parent = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        FireworksPosition();
        accelerate();
        Move();
        UII.Score(this.gameObject, playerFireworks.Count);
        
	}
    void Move()
    {
        this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
        if (Input.GetMouseButton(0) && FireworkScripts.GG != 1 && UIScript.isWin != 1)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            this.transform.localPosition = Vector3.MoveTowards(this.transform.position, hit.point, moveSpeed);
            this.transform.LookAt(new Vector3(hit.point.x, this.transform.position.y, hit.point.z));
            Idle = 1;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Idle = 0;
        }
        if (Idle == 0)
        {
            i++;
            this.transform.rotation = idleFace;
            this.transform.Rotate(new Vector3(-90, 0, 0));
            ParticleSystemBlue.gameObject.SetActive(true);
            ParticleSystemRed.gameObject.SetActive(true);
            ParticleSystemYellow.gameObject.SetActive(true);
            if (i < 60)
            {
                ParticleSystemBlue.GetComponent<Transform>().localScale = new Vector3(i + 30 / 60f * 1f, i + 30 / 60f * 1f, i + 30 / 60f * 1f);
                ParticleSystemRed.GetComponent<Transform>().localScale = new Vector3(i + 30 / 60f * 2f, i + 30 / 60f * 2f, i + 30 / 60f * 2f);
                ParticleSystemYellow.GetComponent<Transform>().localScale = new Vector3(i + 30 / 60f * 3f, i + 30 / 60f * 3f, i + 30 / 60f * 3f);
            }
            if (i >= 60)
            {
                i = 0;
            }
        }
        if (Idle == 1)
        {
            i = 0;
            
            ParticleSystemBlue.gameObject.SetActive(false);
            ParticleSystemRed.gameObject.SetActive(false);
            ParticleSystemYellow.gameObject.SetActive(false);
        }
    }

    void accelerate()
    {
        if (accelerateTime < 180)
        {
            accelerateTime++;
            moveSpeed = 0.3f;
        }
        else
        {
            moveSpeed = 0.2f;
        }
    }

    void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void FireworksPosition()
    {
        if (fireworksRound <= Mathf.PI * 2)
        {
            fireworksRound += 6.0f / 360.0f * Mathf.PI * 2;
        }
        else if (fireworksRound > Mathf.PI * 2)
        {
            fireworksRound = 0;
        }
        if (playerFireworks.Count > 0)
        {
            for (int i = 0; i < playerFireworks.Count; i++)
            {
                if (Idle == 1)
                {
                    playerFireworks[i].transform.localPosition = Vector3.MoveTowards(playerFireworks[i].transform.localPosition, new Vector3(this.transform.position.x + 3f * Mathf.Sin(fireworksRound + Mathf.PI * 2 * (float)i / (float)playerFireworks.Count), 0.5f, this.transform.position.z + 3f * Mathf.Cos(fireworksRound + Mathf.PI * 2 * (float)i / (float)playerFireworks.Count)), 1f);
                }
                if (Idle == 0)
                {
                    playerFireworks[i].transform.localPosition = Vector3.MoveTowards(playerFireworks[i].transform.localPosition, new Vector3(this.transform.position.x + 3f * Mathf.Sin(fireworksRound + Mathf.PI * 2 * (float)i / (float)playerFireworks.Count), 1f, this.transform.position.z + 3f * Mathf.Cos(fireworksRound + Mathf.PI * 2 * (float)i / (float)playerFireworks.Count)), 1f);
                }
            }
        }
    }
    void OnCollisionStay(Collision other)
    {
        if (other.collider.tag == "Fireworks")
        {
            groundFireworks.GroundALLFireworks.Remove(other.gameObject);
            other.transform.tag = "PlayerFireworks";
            other.gameObject.layer = 10;
            playerFireworks.Add(other.gameObject);
            Music.GetFireworks.Play();
        }
        if (other.collider.tag == "Rocket")
        {
            groundFireworks.GroundALLRocket.Remove(other.gameObject);
            Destroy(other.gameObject);
            accelerateTime = 0;
        }
    }
}
