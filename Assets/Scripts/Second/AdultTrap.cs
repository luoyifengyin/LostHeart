using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Second
{
    public class AdultTrap : MonoBehaviour
    {
        public GameObject m_Audio;
        public int m_RemoveTime = 10;//陷阱消失时间
        public float m_Speed = 0.1f;//陷阱速度
        public int m_Direction = -1;//陷阱方向 0->前，1->后，2->左，3->右，4->上，5->下

        private int m_NowTime = 0;//陷阱存在时间
        private bool m_IsDestroy=false;//陷阱是否去除
        private string m_LeapName = "Cube";//主角名字
        void Start()
        {
            turn();//陷阱方向初始化
            m_Audio = GameObject.Find("SoundEffects");
        }

        
        void Update()
        {
            Move();//陷阱的移动
            Removed();//陷阱的去除
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag=="Player")
            {
                GameObject.Find(m_LeapName).GetComponent<AdultLead>().m_Heal--;
                Destroy(this.gameObject);
                m_Audio.GetComponent<AdultSoundEffects>().Health();
            }

            if (other.gameObject.tag == "Car")
            {
                
                Destroy(this.gameObject);
            }

            if (other.gameObject.tag == "GameController")
            {

                Destroy(this.gameObject);
            }
        }

        void Move()
        {
            this.transform.Translate(new Vector3(0, 0, m_Speed));
        }


        void turn()
        {
            if(m_NowTime < m_RemoveTime)
            if(m_Direction == 0)
            {
                this.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
            }
            else if (m_Direction == 1)
            {
                this.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
            }
            else if (m_Direction == 2)
            {
                this.transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 0));
            }
            else if (m_Direction == 3)
            {
                this.transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0));
            }
            else if (m_Direction == 4)
            {
                this.transform.rotation = Quaternion.LookRotation(new Vector3(0, 1, 0));
            }
            else if (m_Direction == 5)
            {
                this.transform.rotation = Quaternion.LookRotation(new Vector3(0, -1, 0));
            }
        }

        void Removed()
        {
            if(m_NowTime>=m_RemoveTime)
            {
                m_IsDestroy = true;
            }
            if(m_IsDestroy==true)
            {
                Destroy(this.gameObject);
            }
            m_NowTime++;
        }

        public void Fix(int RemoveTime, float Speed, int Direction, float x, float y, float z)
        {
            m_RemoveTime = RemoveTime;
            m_Speed = Speed;
            m_Direction = Direction;
            this.transform.position = new Vector3(x, y, z);
            turn();
        }
    }
}