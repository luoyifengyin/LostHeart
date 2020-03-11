using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameApplication.Second;

namespace MyGameApplication.Second
{
    public class AdultScene : MonoBehaviour
    {
        public GameObject m_TrapGameObject;//陷阱预制体
        public Canvas m_Canvas;//陷阱预制体
        public List<bool> m_LevelBegin;//各关卡是否开始
        public List<int> m_LevelTime;//关卡时间轴
        public List<GameObject> m_LevelDoor;//下一关门口
        public GameObject m_Trap;//陷阱管理

        private int m_LevelNumber = 4;//关卡数
        private float m_PositionInterval = 2.5f;//位置间隔常量
        private int m_TimeInterval = 13;//时间间隔常量
        private int[] m_TemporaryVariableInt = new int[10];//int临时变量
        private float[] m_TemporaryVariableFloat = new float[10];//float临时变量
        private int[] m_LevelSubsection = new int[10];//关卡临时小节
        void Start()
        {
            NewGame();//游戏初始化
            //SetTrap(1000, 0.1f, 3, 2, 2, 18);
        }


        void Update()
        {
            Level();//关卡总管理
        }

        void NewGame()
        {
            for (int i = 0; i < m_LevelNumber; i++)
            {
                bool a = false;
                int b = 0;
                m_LevelBegin.Add(a);
                m_LevelTime.Add(b);
            }
            for (int i = 0; i < 10; i++)
            {
                m_LevelSubsection[i] = 0;
            }

            m_TemporaryVariableInt[0] = 600;
            m_TemporaryVariableFloat[0] = -27.5f;
            m_TemporaryVariableInt[1] = 925;
            m_TemporaryVariableFloat[1] = 32.5f;
            m_TemporaryVariableInt[2] = 1450;
            m_TemporaryVariableFloat[2] = 331.0f;
            m_TemporaryVariableInt[3] = 1775;
            m_TemporaryVariableFloat[3] = -27.5f;
            m_TemporaryVariableInt[4] = 1775;
            m_TemporaryVariableFloat[4] = 32.5f;
        }

        void SetTrap(int RemoveTime, float Speed, int Direction, float x, float y, float z)//陷阱方向 0->前，1->后，2->左，3->右，4->上，5->下
        {
            GameObject goClone = GameObject.Instantiate(m_TrapGameObject);
            goClone.transform.parent = m_Trap.gameObject.transform;
            goClone.GetComponent<AdultTrap>().Fix(RemoveTime, Speed, Direction, x, y, z);
        }

        void Level()
        {
            if (m_LevelBegin[0] == true)
            {
                LevelOne();
            }
            else if (m_LevelBegin[1] == true)
            {
                LevelTwo();
            }
            else if (m_LevelBegin[2] == true)
            {
                LevelThree();
            }
            else if (m_LevelBegin[3] == true)
            {
                LevelFour();
            }
        }

        void LevelOne()
        {
            float m = -27.5f;
            if (m_LevelTime[0] == 60)
            {
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 1);
                for (int i = 0; i < 25; i++)
                {

                    SetTrap(500, 0.2f, 1, m, 2.5f, 60.0f);
                    m = m + m_PositionInterval;
                }

            }
            else if (m_LevelTime[0] == 360)
            {
                m = 43.6f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(2, 2);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 2, 45.8f, 2.5f, m);
                    m = m - m_PositionInterval;
                }
            }
            else if (m_LevelTime[0] == 660)
            {
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 3);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 0, m, 2.5f, -30.0f);
                    m = m + m_PositionInterval;
                }
            }
            else if (m_LevelTime[0] == 960)
            {
                m = 43.6f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(0, 0);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 3, -42.0f, 2.5f, m);
                    m = m - m_PositionInterval;
                }
            }
            else if (m_LevelTime[0] == 1260)
            {
                Destroy(m_LevelDoor[0]);
                m_LevelBegin[0] = false;
            }
            m_LevelTime[0]++;
        }

        void LevelTwo()
        {
            float m = 0.0f;
            if (m_LevelTime[1] == 120)
            {
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 5);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 1, m, 2.5f, 156.0f);
                    m = m + m_PositionInterval;
                }
                m = 140.0f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(2, 6);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 2, 45.8f, 2.5f, m);
                    m = m - m_PositionInterval;
                }
            }

            if (m_LevelTime[1] == 420)
            {
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 7);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 0, m, 2.5f, 66.0f);
                    m = m + m_PositionInterval;
                }
                m = 140.0f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(0, 4);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 3, -45.8f, 2.5f, m);
                    m = m - m_PositionInterval;
                }
            }

            if (m_LevelTime[1] == 720)
            {
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 5);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 1, m, 2.5f, 156.0f);
                    m = m + m_PositionInterval;
                }
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 7);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 0, m, 2.5f, 66.0f);
                    m = m + m_PositionInterval;
                }
            }

            if (m_LevelTime[1] == 1020)
            {
                m = 140.0f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(2, 6);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 2, 45.8f, 2.5f, m);
                    m = m - m_PositionInterval;
                }
                m = 140.0f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(0, 4);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 3, -45.8f, 2.5f, m);
                    m = m - m_PositionInterval;
                }
            }
            if (m_LevelTime[1] == 1320)
            {
                Destroy(m_LevelDoor[1]);
                m_LevelBegin[1] = false;
            }
            m_LevelTime[1]++;
        }
        void LevelThree()
        {
            float m = -27.5f;
            if (m_LevelTime[2] == 200)
            {
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 9);
                for (int i = 0; i < 13; i++)
                {

                    SetTrap(500, 0.2f, 1, m, 2.5f, 246.0f);
                    m = m + m_PositionInterval;
                }
                m = 235.8f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(2, 10);
                for (int i = 0; i < 13; i++)
                {
                    SetTrap(500, 0.2f, 2, 45.8f, 2.5f, m);
                    m = m - m_PositionInterval;
                }

                m = 2.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 11);
                for (int i = 0; i < 13; i++)
                {
                    SetTrap(500, 0.2f, 0, m, 2.5f, 162.6f);
                    m = m + m_PositionInterval;
                }
                m = 205.8f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(0, 8);
                for (int i = 0; i < 13; i++)
                {
                    SetTrap(500, 0.2f, 3, -42.0f, 2.5f, m);
                    m = m - m_PositionInterval;
                }

            }

            if (m_LevelTime[2] == 500)
            {
                m = 2.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 9);
                for (int i = 0; i < 13; i++)
                {

                    SetTrap(500, 0.2f, 1, m, 2.5f, 246.0f);
                    m = m + m_PositionInterval;
                }
                m = 205.8f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(2, 10);
                for (int i = 0; i < 13; i++)
                {
                    SetTrap(500, 0.2f, 2, 45.8f, 2.5f, m);
                    m = m - m_PositionInterval;
                }

                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 11);
                for (int i = 0; i < 13; i++)
                {
                    SetTrap(500, 0.2f, 0, m, 2.5f, 162.6f);
                    m = m + m_PositionInterval;
                }
                m = 235.8f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(0, 8);
                for (int i = 0; i < 13; i++)
                {
                    SetTrap(500, 0.2f, 3, -42.0f, 2.5f, m);
                    m = m - m_PositionInterval;
                }

            }

            if (m_LevelTime[2] == 1000)
            {
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 9);
                for (int i = 0; i < 25; i++)
                {

                    SetTrap(500, 0.2f, 1, m, 2.5f, 246.0f);
                    m = m + m_PositionInterval;
                }


                m = 235.8f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(2, 10);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 2, 45.8f, 2.5f, m);
                    m = m - m_PositionInterval;
                }

                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 11);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 0, m, 2.5f, 162.6f);
                    m = m + m_PositionInterval;
                }
            }

            if (m_LevelTime[2] == 1400)
            {
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 9);
                for (int i = 0; i < 25; i++)
                {

                    SetTrap(500, 0.2f, 1, m, 2.5f, 246.0f);
                    m = m + m_PositionInterval;
                }


                m = 235.8f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(2, 10);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 2, 45.8f, 2.5f, m);
                    m = m - m_PositionInterval;
                }

                m = 235.8f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(0, 8);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 3, -42.0f, 2.5f, m);
                    m = m - m_PositionInterval;
                }
            }

            if (m_LevelTime[2] == 1900)
            {
                m = 235.8f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(2, 10);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 2, 45.8f, 2.5f, m);
                    m = m - m_PositionInterval;
                }

                m = 235.8f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(0, 8);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 3, -42.0f, 2.5f, m);
                    m = m - m_PositionInterval;
                }

                m = 2.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 9);
                for (int i = 0; i < 13; i++)
                {

                    SetTrap(500, 0.2f, 1, m, 2.5f, 246.0f);
                    m = m + m_PositionInterval;
                }
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 11);
                for (int i = 0; i < 12; i++)
                {
                    SetTrap(500, 0.2f, 0, m, 2.5f, 162.6f);
                    m = m + m_PositionInterval;
                }
            }

            if (m_LevelTime[2] == 2000)
            {
                m = 2.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 9);
                for (int i = 0; i < 13; i++)
                {

                    SetTrap(500, 0.2f, 1, m, 2.5f, 246.0f);
                    m = m + m_PositionInterval;
                }
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 11);
                for (int i = 0; i < 12; i++)
                {
                    SetTrap(500, 0.2f, 0, m, 2.5f, 162.6f);
                    m = m + m_PositionInterval;
                }
            }

            if (m_LevelTime[2] == 2100)
            {
                m = 2.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 9);
                for (int i = 0; i < 13; i++)
                {

                    SetTrap(500, 0.2f, 1, m, 2.5f, 246.0f);
                    m = m + m_PositionInterval;
                }
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 11);
                for (int i = 0; i < 12; i++)
                {
                    SetTrap(500, 0.2f, 0, m, 2.5f, 162.6f);
                    m = m + m_PositionInterval;
                }
            }

            if (m_LevelTime[2] == 2200)
            {
                m = 235.8f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(2, 10);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 2, 45.8f, 2.5f, m);
                    m = m - m_PositionInterval;
                }

                m = 235.8f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(0, 8);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 3, -42.0f, 2.5f, m);
                    m = m - m_PositionInterval;
                }
                m = 2.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 9);
                for (int i = 0; i < 13; i++)
                {

                    SetTrap(500, 0.2f, 1, m, 2.5f, 246.0f);
                    m = m + m_PositionInterval;
                }
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 11);
                for (int i = 0; i < 12; i++)
                {
                    SetTrap(500, 0.2f, 0, m, 2.5f, 162.6f);
                    m = m + m_PositionInterval;
                }

            }

            if (m_LevelTime[2] == 2300)
            {
                m = 2.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 9);
                for (int i = 0; i < 13; i++)
                {

                    SetTrap(500, 0.2f, 1, m, 2.5f, 246.0f);
                    m = m + m_PositionInterval;
                }
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 11);
                for (int i = 0; i < 12; i++)
                {
                    SetTrap(500, 0.2f, 0, m, 2.5f, 162.6f);
                    m = m + m_PositionInterval;
                }
            }

            if (m_LevelTime[2] == 2400)
            {
                m = 2.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 9);
                for (int i = 0; i < 13; i++)
                {

                    SetTrap(500, 0.2f, 1, m, 2.5f, 246.0f);
                    m = m + m_PositionInterval;
                }
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 11);
                for (int i = 0; i < 12; i++)
                {
                    SetTrap(500, 0.2f, 0, m, 2.5f, 162.6f);
                    m = m + m_PositionInterval;
                }
            }

            if (m_LevelTime[2] == 2500)
            {
                m = 235.8f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(2, 10);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 2, 45.8f, 2.5f, m);
                    m = m - m_PositionInterval;
                }

                m = 235.8f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(0, 8);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 3, -42.0f, 2.5f, m);
                    m = m - m_PositionInterval;
                }
                m = 2.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 9);
                for (int i = 0; i < 13; i++)
                {

                    SetTrap(500, 0.2f, 1, m, 2.5f, 246.0f);
                    m = m + m_PositionInterval;
                }
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 11);
                for (int i = 0; i < 12; i++)
                {
                    SetTrap(500, 0.2f, 0, m, 2.5f, 162.6f);
                    m = m + m_PositionInterval;
                }
            }
            if (m_LevelTime[2] == 2800)
            {
                Destroy(m_LevelDoor[2]);
                m_LevelBegin[2] = false;
            }
            m_LevelTime[2]++;
        }
        void LevelFour()
        {
            float m = -27.5f;
            if (m_LevelTime[3] == 300)
            {
                m = 331.1f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(0, 12);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 3, -41.3f, 2.5f, m);
                    m = m - m_PositionInterval;
                }
                m = -27.5f;
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 13);
                for (int i = 0; i < 25; i++)
                {
                    SetTrap(500, 0.2f, 1, m, 2.5f, 345.8f);
                    m = m + m_PositionInterval;
                }
            }


            if (m_LevelTime[3] == 600)
            {
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(2, 13);
            }
            if (m_LevelTime[3] == m_TemporaryVariableInt[0]&& m_LevelTime[3]<925)
            {
                SetTrap(500, 0.2f, 1, m_TemporaryVariableFloat[0], 2.5f, 345.8f);
                m_TemporaryVariableFloat[0] = m_TemporaryVariableFloat[0] + m_PositionInterval;
                m_TemporaryVariableInt[0] = m_TemporaryVariableInt[0] + m_TimeInterval;
            }
            if (m_LevelTime[3] == m_TemporaryVariableInt[1] && m_LevelTime[3] < 1250)
            {
                SetTrap(500, 0.2f, 1, m_TemporaryVariableFloat[1], 2.5f, 345.8f);
                m_TemporaryVariableFloat[1] = m_TemporaryVariableFloat[1] - m_PositionInterval;
                m_TemporaryVariableInt[1] = m_TemporaryVariableInt[1] + m_TimeInterval;
            }

            if (m_LevelTime[3] == 1450)
            {
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(2, 12);
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(3, 14);
            }
            if (m_LevelTime[3] == m_TemporaryVariableInt[2] && m_LevelTime[3] < 1775)
            {
                SetTrap(500, 0.2f, 3, -43.0f, 2.5f, m_TemporaryVariableFloat[2]);
                SetTrap(500, 0.2f, 2, 46.5f, 2.5f, m_TemporaryVariableFloat[2]);
                m_TemporaryVariableFloat[2] = m_TemporaryVariableFloat[2] - m_PositionInterval;
                m_TemporaryVariableInt[2] = m_TemporaryVariableInt[2] + m_TimeInterval;
                //Debug.Log("ASd");
            }

            if (m_LevelTime[3] == 1775)
            {
                m_Canvas.GetComponent<AdultUI>().OneTipsDisplay(1, 15);
            }
            if (m_LevelTime[3] == m_TemporaryVariableInt[3] && m_LevelTime[3] < 1925)
            {
                SetTrap(500, 0.2f, 0, m_TemporaryVariableFloat[3], 2.5f, 258.0f);
                SetTrap(500, 0.2f, 0, m_TemporaryVariableFloat[4], 2.5f, 258.0f);
                m_TemporaryVariableFloat[3] = m_TemporaryVariableFloat[3] + m_PositionInterval;
                m_TemporaryVariableFloat[4] = m_TemporaryVariableFloat[4] - m_PositionInterval;
                m_TemporaryVariableInt[3] = m_TemporaryVariableInt[3] + m_TimeInterval;
                //Debug.Log("ASd");
            }
            m_LevelTime[3]++;
        }
    }
}