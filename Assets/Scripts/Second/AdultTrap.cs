using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameApplication.ObjectPool;
namespace MyGameApplication.Second
{
    public class AdultTrap : MonoBehaviour
    {
        public GameObject m_Audio;
        public int m_RemoveTime = 10;//陷阱消失时间
        public float m_Speed = 10.0f;//陷阱速度
        public int m_Direction = -1;//陷阱方向 0->前，1->后，2->左，3->右，4->上，5->下

        
        private bool m_IsDestroy=false;//陷阱是否去除
        private string m_LeapName = "Cube";//主角名字
        void Start()
        {
            turn();//陷阱方向初始化
            m_Audio = GameObject.Find("SoundEffects");
        }

        
        /*void Update()
        {
            Move();//陷阱的移动
        }*/

        void FixedUpdate()
        {
            Move();//陷阱的移动
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag=="Player")
            {
                GameObject.Find(m_LeapName).GetComponent<AdultLead>().m_Heal--;
                ObjectPoolManager.Instance.Put("Trap", this.gameObject);
                //Destroy(this.gameObject);
                m_Audio.GetComponent<AdultSoundEffects>().Health();
            }

            if (other.gameObject.tag == "Car")
            {
                ObjectPoolManager.Instance.Put("Trap", this.gameObject);
                //Destroy(this.gameObject);
            }

            if (other.gameObject.tag == "GameController")
            {

                ObjectPoolManager.Instance.Put("Trap", this.gameObject);
                //Destroy(this.gameObject);
            }
        }

        void Move()
        {
            //this.transform.Translate(new Vector3(0, 0, m_Speed ));
            this.transform.Translate(new Vector3(0, 0, m_Speed*Time.deltaTime));
        }


        void turn()
        {
            
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