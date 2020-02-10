using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Second
{
    
    public class AdultControl : MonoBehaviour
    {
        public float Speed = 0.1f;//主角速度
        public GameObject m_Camera;//主镜头
        public GameObject m_SecondCamera;//隐藏镜头
        public GameObject m_Lead;//主角隐藏位置
        public GameObject m_BoxGameObject;
        public GameObject m_BoxPrefabs;

        private float x = 0, y = 0;//临时变量
        private float m_X, m_Y;//四个方向键
        private bool m_MouseLeftButtonDown;//鼠标左键
        private bool m_MouseLeftButtonUp;//鼠标左键
        private bool m_MouseLeft;//鼠标左键
        private float m_MouseMove;//鼠标移动
        private bool m_MouseRightButtonDown;//鼠标右键
        private float m_CameraRotate=0;//镜头旋转度
        private float m_CameraRotateDown = 30.0f;//镜头偏转度
        private float m_CameraHeight = 5.0f;//镜头高度
        private float m_CameraBack = -10.0f;//镜头后置度
        private float m_SecondCameraRotateDown = 90.0f;//隐藏镜头偏转度
        private float m_SecondCameraHeight = 30.0f;//隐藏镜头高度



        void Start()
        {

        }

       
        void Update()
        {
            Move();//主角移动
            CameraMove();//镜头移动
        }
        
        void Move()
        {
            
            m_X = Input.GetAxis("Horizontal");
            m_Y = Input.GetAxis("Vertical");
            m_MouseLeftButtonDown = Input.GetMouseButtonDown(0);
            m_MouseLeftButtonUp = Input.GetMouseButtonUp(0);
            m_MouseRightButtonDown = Input.GetMouseButton(1);

            if(m_MouseLeftButtonDown==true && m_MouseLeft==false && this.GetComponent<AdultLead>().m_Box>0)
            {
                Debug.Log("asd");
                m_MouseLeft = true;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.transform.tag=="ItemSprite")
                    {
                        this.GetComponent<AdultLead>().m_Box--;
                        GameObject goClone = GameObject.Instantiate(m_BoxPrefabs);
                        goClone.transform.parent = m_BoxGameObject.gameObject.transform;
                        goClone.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    }
                }
            }

            if(m_MouseLeftButtonUp==true && m_MouseLeft==true)
            {
                m_MouseLeft = false;
            }


            if (m_MouseRightButtonDown == true)
            {
                m_MouseMove=Input.GetAxis("Mouse X");
                m_CameraRotate = m_CameraRotate + m_MouseMove;
            }

            
            //Debug.Log(Input.GetAxis("Horizontal"));
            if (m_Y > 0)
            {
                y = 1;
            }
            else if (m_Y < 0)
            {
                y = -1;
            }
            if (m_X >0)
            {
                x = 1;
            }
            else if (m_X <0)
            {
                x = -1;
            }
            if (m_X != 0 || m_Y != 0)
            { 
                this.transform.rotation = Quaternion.LookRotation(new Vector3(x, 0, y));
                this.transform.Rotate(new Vector3(0, 1, 0), m_CameraRotate); 
            }
            if (x != 0 || y != 0)
            { 
                this.transform.Translate(new Vector3(0, 0, Speed));
            }
            x = 0;
            y = 0;
        }

        void CameraMove()
        {
            m_Lead.transform.position = this.transform.position;

            m_SecondCamera.transform.rotation = m_Lead.transform.rotation;
            m_SecondCamera.transform.position = new Vector3(m_Lead.transform.position.x, m_Lead.transform.position.y + m_SecondCameraHeight, m_Lead.transform.position.z);
            m_SecondCamera.transform.Rotate(new Vector3(0, 1, 0), m_CameraRotate);
            m_SecondCamera.transform.Rotate(new Vector3(1, 0, 0), m_SecondCameraRotateDown);

            m_Camera.transform.rotation = m_Lead.transform.rotation;
            m_Camera.transform.Rotate(new Vector3(0, 1, 0), m_CameraRotate);
            m_Camera.transform.Rotate(new Vector3(1, 0, 0), m_CameraRotateDown);
            m_Camera.transform.position = new Vector3( m_Lead.transform.position.x, m_Lead.transform.position.y+ m_CameraHeight, m_Lead.transform.position.z);
            m_Camera.transform.Translate(new Vector3(0, 0, m_CameraBack));

        }
    }
}