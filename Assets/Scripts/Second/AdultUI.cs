using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGameApplication.Second;
using MyGameApplication.Item.Inventory;

namespace MyGameApplication.Second
{

    public class AdultUI : MonoBehaviour
    {
        public Canvas m_AdultCanvas;//场景Canvas
        public List<Image> m_HealImages;//血量图片
        public List<Image> m_TipImages;//提示图片
        public List<GameObject> m_WallGameObject;//提示位置
        public Camera m_SecondCamera;//第二摄像头
        public List<bool> m_TipsDisplay;//提示是否显示
        public List<int> m_TipsTime;//提示显示时间轴
        public GameObject m_Leap;//主角
        public Text m_BoxText;//箱子数

        private float m_AdultCanvasWeight=547.0f;//Canvas宽带
        private float m_AdultCanvasHigh=217.0f;//Canvas高度
        void Start()
        {
            NewGame();//游戏初始化

        }

        
        void Update()
        {
            HealDisplay();//显示生命值
            BoxDisplay();//显示生命值
            TipDisplay();//显示提示
        }

        void HealDisplay() 
        {
            if (m_Leap.GetComponent<AdultLead>().m_Heal == 3)
            {
                m_HealImages[0].enabled = true;
                m_HealImages[1].enabled = true;
                m_HealImages[2].enabled = true;
            }
            else if (m_Leap.GetComponent<AdultLead>().m_Heal == 2)
            {
                m_HealImages[0].enabled = true;
                m_HealImages[1].enabled = true;
                m_HealImages[2].enabled = false;
            }
            else if (m_Leap.GetComponent<AdultLead>().m_Heal == 1)
            {
                m_HealImages[0].enabled = true;
                m_HealImages[1].enabled = false;
                m_HealImages[2].enabled = false;
            }
            else if (m_Leap.GetComponent<AdultLead>().m_Heal == 0)
            {
                m_HealImages[0].enabled = false;
                m_HealImages[1].enabled = false;
                m_HealImages[2].enabled = false;
            }
            else
            { }
        }

        void BoxDisplay()
        {
            m_BoxText.text = PlayerBag.Instance.GetCnt(AdultLead.ITEM_BOX_ID).ToString();
        }

        void TipDisplay()
        {
            for (int i = 0; i < 4; i++)
            {
                m_TipsTime[i]++;

                if (m_TipsDisplay[i] == true &&
                    ((m_TipsTime[i] >= 0 && m_TipsTime[i] <= 30) || (m_TipsTime[i] >= 60 && m_TipsTime[i] <= 90) || (m_TipsTime[i] >= 120 && m_TipsTime[i] <= 150))
                    )
                {
                    m_TipImages[i].gameObject.SetActive(true);
                    m_TipImages[i].transform.position = RectTransformUtility.WorldToScreenPoint(m_SecondCamera, m_WallGameObject[i].transform.position);
                    Vector2 a = m_TipImages[i].GetComponent<RectTransform>().anchoredPosition;

                    if (a.x > m_AdultCanvasWeight || a.x < -m_AdultCanvasWeight || a.y > m_AdultCanvasHigh || a.y < -m_AdultCanvasHigh)
                    {
                        if (a.x > m_AdultCanvasWeight)
                        {
                            a = new Vector2(m_AdultCanvasWeight, a.y);
                        }
                        if (a.x < -m_AdultCanvasWeight)
                        {
                            a = new Vector2(-m_AdultCanvasWeight, a.y);
                        }
                        if (a.y > m_AdultCanvasHigh)
                        {
                            a = new Vector2(a.x, m_AdultCanvasHigh);
                        }
                        if (a.y < -m_AdultCanvasHigh)
                        {
                            a = new Vector2(a.x, -m_AdultCanvasHigh);
                        }
                        m_TipImages[i].GetComponent<RectTransform>().anchoredPosition = a;
                    }
                }
                else
                {
                    m_TipImages[i].gameObject.SetActive(false);
                    if (m_TipsTime[i] > 150)
                    {
                        m_TipsDisplay[i] = false;
                    }
                }
            }
            
        }

        void NewGame()
        {
            for (int i = 0; i < 4; i++)
            {
                bool a = false;
                m_TipsDisplay.Add(a);
            }
            for (int i = 0; i < 4; i++)
            {
                int a = 2000;
                m_TipsTime.Add(a);
            }
        }

        public void OneTipsDisplay(int Tips ,int TipsPosition)//0前1后2左3右,0左1前2右3后
        {
            if(m_TipsDisplay[Tips]==false)
            {
                m_WallGameObject[Tips] = m_WallGameObject[TipsPosition];
                m_TipsDisplay[Tips] = true;
                m_TipsTime[Tips] = 0;
            }
        }

        public void Onclick()
        { }

        

    }
}