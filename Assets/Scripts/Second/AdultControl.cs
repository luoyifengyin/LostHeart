using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Second
{
    
    public class AdultControl : MonoBehaviour
    {
        public float Speed = 0.1f;//主角速度
        public GameObject m_Camera;//主镜头
        public GameObject m_Lead;//主角隐藏位置

        private float x = 0, y = 0;//临时变量
        private bool m_W, m_S, m_A, m_D;//四个方向键
        private float m_MouseMove;//鼠标移动
        private bool m_MouseRightButtonDown;//鼠标右键
        private float m_CameraRotate=0;//镜头旋转度
        private float m_CameraRotateDown = 30.0f;//镜头偏转度
        private float m_CameraHeight = 5.0f;//镜头高度
        private float m_CameraBack = -10.0f;//镜头后置度

        

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
            
            m_W = Input.GetKey(KeyCode.W);
            m_S = Input.GetKey(KeyCode.S);
            m_A = Input.GetKey(KeyCode.A);
            m_D = Input.GetKey(KeyCode.D);

            m_MouseRightButtonDown = Input.GetMouseButton(1);
            if (m_MouseRightButtonDown == true)
            {
                m_MouseMove=Input.GetAxis("Mouse X");
                m_CameraRotate = m_CameraRotate + m_MouseMove;
            }

            
            //Debug.Log(Input.GetAxis("Horizontal"));
            if (m_W == true)
            {
                y = 1;
            }
            else if (m_S == true)
            {
                y = -1;
            }
            if (m_D == true)
            {
                x = 1;
            }
            else if (m_A == true)
            {
                x = -1;
            }
            if (m_W == true || m_S == true || m_A == true || m_D == true)
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
            m_Camera.transform.rotation = m_Lead.transform.rotation;
            m_Camera.transform.Rotate(new Vector3(0, 1, 0), m_CameraRotate);
            m_Camera.transform.Rotate(new Vector3(1, 0, 0), m_CameraRotateDown);
            m_Camera.transform.position = new Vector3( m_Lead.transform.position.x, m_Lead.transform.position.y+ m_CameraHeight, m_Lead.transform.position.z);
            m_Camera.transform.Translate(new Vector3(0, 0, m_CameraBack));

        }
    }
}