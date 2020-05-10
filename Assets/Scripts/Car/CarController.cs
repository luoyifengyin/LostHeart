using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Car {
    internal enum CarDriveType {
        FourWheelDrive,     //四轮驱动
        RearWheelDrive,     //后轮驱动
        FrontWheelDrive,    //前轮驱动
    }

    public class CarController : MonoBehaviour {
        [SerializeField] private CarDriveType m_DriveType = CarDriveType.FourWheelDrive;    //驱动方式
        [SerializeField] private WheelCollider[] m_WheelColliders = new WheelCollider[4];
        [SerializeField] private GameObject[] m_WheelMeshes = new GameObject[4];
        [SerializeField] private Vector3 m_CenterOfMassOffset = new Vector3(0, 0, 0);       //质心偏移量
        [SerializeField] private float m_MaxTurnAngle = 30;                                    //转弯角度
        [SerializeField] private float m_ForwardTorque = 2500;                              //前进车轮扭矩
        [SerializeField] private float m_BackwardTorque = 500;                              //后退车轮扭矩
        [SerializeField] private float m_TractionStep = 10;                                 //牵引力递进量
        //[SerializeField] private float m_MaxSpeed = 100;                                    //最大速度
        [SerializeField] private float m_BrakeTorque = float.MaxValue;                      //刹车扭矩
        [SerializeField] private float m_DownForce = 100;                                   //增大抓地力
        //[SerializeField] private bool m_UseAntiRollBar = false;                             //是否使用平衡杆
        [SerializeField] private float m_AntiRoll = 0;

        private Rigidbody m_Rb;
        private float m_CurTorque = 0;

        public float CurrentSpeed { get { return m_Rb.velocity.magnitude; } }
        public float AccelInput { get; private set; }
        public bool Braking { get; private set; }
        public float SteerAngle { get; private set; }
        public WheelCollider[] WheelColliders { get { return m_WheelColliders; } }
        public float ForwardTorque {
            get => m_ForwardTorque;
            set => m_ForwardTorque = value;
        }
        public float CurrentTorque => m_CurTorque;

        private void Awake() {
            if (Mathf.Approximately(m_AntiRoll, 0)) return;
            AntiRollBar[] antiRollBars = GetComponents<AntiRollBar>();
            if (antiRollBars.Length != 2) {
                foreach (var bar in antiRollBars) {
                    Destroy(bar);
                }
                for (int i = 0; i < 2; i++) {
                    gameObject.AddComponent<AntiRollBar>()
                        .SetWheels(m_WheelColliders[i << 1], m_WheelColliders[i << 1 | 1])
                        .SetAntiRoll(m_AntiRoll);
                }
            }
        }

        // Start is called before the first frame update
        void Start() {
            m_Rb = GetComponent<Rigidbody>();
            m_WheelColliders[0].attachedRigidbody.centerOfMass += m_CenterOfMassOffset;
        }

        // Update is called once per frame
        void Update() {
            for (int i = 0; i < 4; i++) {
                Vector3 pos;
                Quaternion quat;
                m_WheelColliders[i].GetWorldPose(out pos, out quat);
                m_WheelMeshes[i].transform.position = pos;
                m_WheelMeshes[i].transform.rotation = quat;
            }
        }

        private int GetDirection() {
            if (Mathf.Abs(CurrentSpeed) < Mathf.Epsilon) return 0;
            if (Vector3.Angle(transform.forward, m_Rb.velocity) < 90f)
                return 1;
            else return -1;
        }

        public void Move(float h, float v) {
            SteerAngle = h * m_MaxTurnAngle;
            m_WheelColliders[0].steerAngle = SteerAngle;
            m_WheelColliders[1].steerAngle = SteerAngle;

            AccelInput = v;
            if (v > 0) {
                m_CurTorque += m_TractionStep;
                if (m_CurTorque > m_ForwardTorque) m_CurTorque = m_ForwardTorque;
                //m_CurTorque = m_ForwardTorque;
            }
            else if (v < 0) {
                m_CurTorque = m_BackwardTorque;
            }
            else {
                m_CurTorque -= m_TractionStep;
                if (m_CurTorque < 0) m_CurTorque = 0;
            }

            float accel = v * m_CurTorque;
            if (GetDirection() * v < 0 && Mathf.Abs(accel) * Time.fixedDeltaTime * Time.fixedDeltaTime < CurrentSpeed) {
                Brake(v);
            }
            else {
                for (int i = 0; i < 4; i++) {
                    m_WheelColliders[i].brakeTorque = 0;
                }
                Braking = false;
            }
            ApplyDrive(accel);

            //LimitSpeedByDrag();
        }

        private void ApplyDrive(float torque) {
            int st = 0, cnt = 0;
            switch (m_DriveType) {
                case CarDriveType.FourWheelDrive:
                    st = 0; cnt = 4;
                    break;
                case CarDriveType.FrontWheelDrive:
                    st = 0; cnt = 2;
                    break;
                case CarDriveType.RearWheelDrive:
                    st = 2; cnt = 2;
                    break;
            }
            for (int i = st; i < st + cnt; i++) {
                m_WheelColliders[i].motorTorque = torque / cnt;
            }
        }

        //改变摩擦力以稳定速度
        //private void LimitSpeedByDrag() {
        //    if (AccelInput != 0) m_Rb.drag = m_Rb.velocity.magnitude / m_MaxSpeed;
        //    else m_Rb.drag = m_OriginalDrag;
        //}

        public void Brake(float mul = 1) {
            mul = Mathf.Clamp01(Mathf.Abs(mul));
            if (mul > 0) Braking = true;
            for (int i = 0; i < 4; i++) {
                m_WheelColliders[i].brakeTorque = mul * m_BrakeTorque;
            }
        }

        private void AddDownForce() {
            m_WheelColliders[0].attachedRigidbody.AddForce(-transform.up * m_DownForce * CurrentSpeed);
        }

        private void FixedUpdate() {
            AddDownForce();
        }
    }
}
