using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameApplication.Second;

namespace MyGameApplication.Second
{
    public class AdultLead : MonoBehaviour
    {
        public int m_Heal = 3;//主角生命值
        public int m_Box = 0;//主角箱子数
        public List<GameObject> m_LevelBeginGameObject;//关卡开始区域
        public GameObject m_Scene;//场景管理
        public GameObject m_Trap;//陷阱管理
        // Start is called before the first frame update
        void Start()
        {

        }


        void Update()
        {
            IsDeath();//判断主角是否死亡

        }
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == m_LevelBeginGameObject[0])
            {
                m_Heal = 3;
                m_Scene.GetComponent<AdultScene>().m_LevelTime[0] = 0;
                m_Scene.GetComponent<AdultScene>().m_LevelBegin[0] = true;
                m_LevelBeginGameObject[0].gameObject.SetActive(false);
            }

            if (other.gameObject == m_LevelBeginGameObject[1])
            {
                m_Box = 1;
                m_Heal = 3;
                m_Scene.GetComponent<AdultScene>().m_LevelTime[1] = 0;
                m_Scene.GetComponent<AdultScene>().m_LevelBegin[1] = true;
                m_LevelBeginGameObject[1].gameObject.SetActive(false);
                
            }

            if (other.gameObject == m_LevelBeginGameObject[2])
            {
                m_Box = 2;
                m_Heal = 3;
                m_Scene.GetComponent<AdultScene>().m_LevelTime[2] = 0;
                m_Scene.GetComponent<AdultScene>().m_LevelBegin[2] = true;
                m_LevelBeginGameObject[2].gameObject.SetActive(false);
            }

            if (other.gameObject == m_LevelBeginGameObject[3])
            {
                m_Box = 1;
                m_Heal = 3;
                m_Scene.GetComponent<AdultScene>().m_LevelTime[3] = 0;
                m_Scene.GetComponent<AdultScene>().m_LevelBegin[3] = true;
                m_LevelBeginGameObject[3].gameObject.SetActive(false);
            }

            if (other.gameObject.tag == "GameController")
            {
                other.transform.Translate(Vector3.back);
            }
        }

        void IsDeath()
        {
            if (m_Heal == 0)
            {
                m_Heal = 3;
                m_Box = 0;
                Replay();
                m_Trap.GetComponent<AdultTrapAdministration>().CleanTrap();
            }
        }

        void Replay()
        {
            if(m_Scene.GetComponent<AdultScene>().m_LevelBegin[0]==true)
            {
                this.transform.position = new Vector3(3.7f, 2.6f, -48.5f);
                m_LevelBeginGameObject[0].gameObject.SetActive(true);
                m_Scene.GetComponent<AdultScene>().m_LevelBegin[0] = false;
                m_Scene.GetComponent<AdultScene>().m_LevelTime[0] = 0;
            }
            if (m_Scene.GetComponent<AdultScene>().m_LevelBegin[1] == true)
            {
                this.transform.position = new Vector3(3.7f, 2.6f, 58.5f);
                m_LevelBeginGameObject[1].gameObject.SetActive(true);
                m_Scene.GetComponent<AdultScene>().m_LevelBegin[1] = false;
                m_Scene.GetComponent<AdultScene>().m_LevelTime[1] = 0;
            }
            if (m_Scene.GetComponent<AdultScene>().m_LevelBegin[2] == true)
            {
                this.transform.position = new Vector3(3.7f, 2.6f, 148.5f);
                m_LevelBeginGameObject[2].gameObject.SetActive(true);
                m_Scene.GetComponent<AdultScene>().m_LevelBegin[2] = false;
                m_Scene.GetComponent<AdultScene>().m_LevelTime[2] = 0;
            }
            if (m_Scene.GetComponent<AdultScene>().m_LevelBegin[3] == true)
            {
                this.transform.position = new Vector3(3.7f, 2.6f, 238.5f);
                m_LevelBeginGameObject[3].gameObject.SetActive(true);
                m_Scene.GetComponent<AdultScene>().m_LevelBegin[3] = false;
                m_Scene.GetComponent<AdultScene>().m_LevelTime[3] = 0;
            }
        }
    }
}