using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameApplication.Second;

namespace MyGameApplication.Second
{
    public class AdultScene : MonoBehaviour
    {
        public GameObject m_TrapGameObject;//陷阱预制体

        private int m_LevelOneTime = 0;
        private bool m_LevelOneBegin = false;
        void Start()
        {
            m_LevelOneBegin = true;
            //SetTrap(1000, 0.1f, 3, 2, 2, 18);
        }

        
        void Update()
        {
            Level();
        }

        void SetTrap(int RemoveTime, float Speed, int Direction, float x, float y, float z)
        {
            //Debug.Log("asd");
            GameObject goClone = GameObject.Instantiate(m_TrapGameObject);
            goClone.GetComponent<AdultTrap>().Fix(RemoveTime, Speed, Direction, x, y, z);
        }

        void Level()
        { 
            if(m_LevelOneBegin==true)
            {
                LevelOne();
            }

        }

        void LevelOne()
        {
            if(m_LevelOneTime==60)
            {
                SetTrap(1000, 0.1f, 1, -5.0f, 2.5f, -10.0f);
                SetTrap(1000, 0.1f, 1, -2.5f, 2.5f, -10.0f);
                SetTrap(1000, 0.1f, 1, 0.0f, 2.5f, -10.0f);
                SetTrap(1000, 0.1f, 1, 2.5f, 2.5f, -10.0f);
                SetTrap(1000, 0.1f, 1, 5.0f, 2.5f, -10.0f);
            }
            m_LevelOneTime++;
            //Debug.Log("asd");
        }
    }
}