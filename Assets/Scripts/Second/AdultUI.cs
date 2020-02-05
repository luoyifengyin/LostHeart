using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGameApplication.Second;

namespace MyGameApplication.Second
{
    public class AdultUI : MonoBehaviour
    {
        public Canvas m_AdultCanvas;
        public List<Image> m_HealImages; 
        void Start()
        {

        }

        
        void Update()
        {
            HealDisplay();//显示生命值
        }

        void HealDisplay() 
        {
            if (AdultLead.s_Heal == 3)
            {
                m_HealImages[0].enabled = true;
                m_HealImages[1].enabled = true;
                m_HealImages[2].enabled = true;
            }
            else if (AdultLead.s_Heal == 2)
            {
                m_HealImages[0].enabled = true;
                m_HealImages[1].enabled = true;
                m_HealImages[2].enabled = false;
            }
            else if (AdultLead.s_Heal == 1)
            {
                m_HealImages[0].enabled = true;
                m_HealImages[1].enabled = false;
                m_HealImages[2].enabled = false;
            }
            else if (AdultLead.s_Heal == 0)
            {
                m_HealImages[0].enabled = false;
                m_HealImages[1].enabled = false;
                m_HealImages[2].enabled = false;
            }
            else
            { }
        }
    }
}