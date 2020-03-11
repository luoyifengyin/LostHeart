using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyGameApplication.Second
{
    public class AdultTrapAdministration : MonoBehaviour
    {
        public GameObject m_BoxGameObject;//箱子预制体
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public void CleanTrap()
        {
            for (int i = 0; i < this.transform.childCount; i++)
            { 
                Destroy( this.transform.GetChild(i).gameObject); 
            }
            for (int i = 0; i < m_BoxGameObject.transform.childCount; i++)
            {
                Destroy(m_BoxGameObject.transform.GetChild(i).gameObject);
            }
        }
    }
}