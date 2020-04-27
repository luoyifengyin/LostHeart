using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameApplication.Second;
using MyGameApplication.UI;
using MyGameApplication.Item.Inventory;
using MyGameApplication.Item;

namespace MyGameApplication.Second
{
    public class AdultLead : MonoBehaviour
    {
        public GameObject m_Moster;//怪物
        public int m_Heal = 3;//主角生命值
        //public int m_Box = 0;//主角箱子数
        public static readonly int ITEM_BOX_ID = 3;
        public int m_Love = 0;//主角箱子数
        public List<GameObject> m_LevelBeginGameObject;//关卡开始区域
        public AdultScene m_AdultScene;//场景管理
        public AdultTrapAdministration m_TrapAdmin;//陷阱管理
        public int m_Stage = 1;//阶段

        // Start is called before the first frame update
        void Start()
        {
            m_Love = 0;
        }


        void Update()
        {
            IsDeath();//判断主角是否死亡
            //Debug.Log(m_Love);
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == m_LevelBeginGameObject[0])
            {
                m_Heal = 3;
                m_AdultScene.m_LevelTime[0] = 0;
                m_AdultScene.m_LevelBegin[0] = true;
                m_LevelBeginGameObject[0].gameObject.SetActive(false);
            }

            if (other.gameObject == m_LevelBeginGameObject[1])
            {

                Dialogue.Caption.ShowDialogue("[点击鼠标左键可放置箱子]",new Color(0,0,1f));
                PlayerBag.Instance.AddProp(ITEM_BOX_ID, 1);
                m_Heal = 3;
                m_AdultScene.m_LevelTime[1] = 0;
                m_AdultScene.m_LevelBegin[1] = true;
                m_LevelBeginGameObject[1].gameObject.SetActive(false);
                
            }

            if (other.gameObject == m_LevelBeginGameObject[2])
            {
                PlayerBag.Instance.AddProp(ITEM_BOX_ID, 2);
                m_Heal = 3;
                m_AdultScene.m_LevelTime[2] = 0;
                m_AdultScene.m_LevelBegin[2] = true;
                m_LevelBeginGameObject[2].gameObject.SetActive(false);
            }

            if (other.gameObject == m_LevelBeginGameObject[3])
            {
                Dialogue.Caption.ShowDialogue("想过来？先让我看看你的能耐吧。", new Color(1.0f, 0, 0));
                PlayerBag.Instance.AddProp(ITEM_BOX_ID, 1);
                m_Heal = 3;
                m_AdultScene.m_LevelTime[3] = 0;
                m_AdultScene.m_LevelBegin[3] = true;
                m_LevelBeginGameObject[3].gameObject.SetActive(false);
            }

            if (other.gameObject == m_LevelBeginGameObject[4])
            {
                if (m_Stage == 1)
                {
                    m_AdultScene.m_LevelTime[4] = 0;
                    m_AdultScene.m_LevelBegin[4] = true;
                    m_Stage++;
                }
                if(m_Stage==2 && PlayerBag.Instance.GetCnt(ITEM_BOX_ID)==0 && m_AdultScene.m_LevelBegin[4] == false)
                {
                    Dialogue.main.ShowDialogue("我喜欢什么？把你一路过来庇护你的东西交给我我再告诉你。", new Color(1.0f, 0, 0));
                }
                if (m_Stage == 2 && PlayerBag.Instance.GetCnt(ITEM_BOX_ID) >= 1 && m_AdultScene.m_LevelBegin[4] == false)
                {
                    //m_Box=0;
                    Dialogue.main.ShowDialogue("我喜欢的东西？那当然是你最珍爱的东西。", new Color(1.0f, 0, 0));
                    m_Stage++;
                }
                if (m_Stage == 3 && m_Love == 0 )
                {
                    Dialogue.main.ShowDialogue("我喜欢的东西？那当然是你最珍爱的东西。", new Color(1.0f, 0, 0));
                }
                if (m_Stage == 3 && m_Love == 1)
                {
                    m_Love--;
                    Dialogue.main.ShowDialogue("哼哼，不错的觉悟，走了。", new Color(1.0f, 0, 0));
                    m_Stage++;
                    Destroy(m_Moster.gameObject);
                }
            }
            if (other.gameObject == m_LevelBeginGameObject[5])
            {
                Manager.SceneController.LoadScene("Maze");
            }

            if (other.gameObject.tag=="Car" && m_Stage==2)
            {
                PlayerBag.Instance.AddProp(ITEM_BOX_ID);
                Dialogue.main.ShowDialogue("[获得道具 小熊]", new Color(0, 0, 1.0f));
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "Finish" && m_Stage == 3)
            {
                m_Love++;
                Dialogue.main.ShowDialogue("[获得道具 小熊]", new Color(0, 0, 1.0f));
                Destroy(other.gameObject);
            }


            /*if (other.gameObject.tag == "GameController")
            {
                other.transform.Translate(Vector3.back);
            }*/
        }

        void IsDeath()
        {
            if (m_Heal == 0)
            {
                m_Heal = 3;
                PlayerBag.Instance.ClearItem(ITEM_BOX_ID);
                Replay();
                m_TrapAdmin.CleanTrap();
            }
        }

        void Replay()
        {
            if(m_AdultScene.m_LevelBegin[0]==true)
            {
                this.transform.position = new Vector3(3.7f, 2.6f, -48.5f);
                m_LevelBeginGameObject[0].gameObject.SetActive(true);
                m_AdultScene.m_LevelBegin[0] = false;
                m_AdultScene.m_LevelTime[0] = 0;
            }
            if (m_AdultScene.m_LevelBegin[1] == true)
            {
                this.transform.position = new Vector3(3.7f, 2.6f, 58.5f);
                m_LevelBeginGameObject[1].gameObject.SetActive(true);
                m_AdultScene.m_LevelBegin[1] = false;
                m_AdultScene.m_LevelTime[1] = 0;
            }
            if (m_AdultScene.m_LevelBegin[2] == true)
            {
                this.transform.position = new Vector3(3.7f, 2.6f, 148.5f);
                m_LevelBeginGameObject[2].gameObject.SetActive(true);
                m_AdultScene.m_LevelBegin[2] = false;
                m_AdultScene.m_LevelTime[2] = 0;
            }
            if (m_AdultScene.m_LevelBegin[3] == true)
            {
                this.transform.position = new Vector3(3.7f, 2.6f, 238.5f);
                m_LevelBeginGameObject[3].gameObject.SetActive(true);
                m_AdultScene.m_LevelBegin[3] = false;
                m_AdultScene.m_LevelTime[3] = 0;
            }
        }
    }
}