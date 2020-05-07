using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform m_Transfrom;
    private Transform m_PlayerTransfrom;
    public float CameraSpeed = 0.3f;
    float ZhuangTai = 0;
    // Use this for initialization
    void Start()
    {
        m_Transfrom = gameObject.GetComponent<Transform>();
        m_PlayerTransfrom = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Transform>();
        Screen.SetResolution(108*4, 192*4, false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LateUpdate()
    {
        if (ZhuangTai == 0)
        {
            m_Transfrom.position = Vector3.MoveTowards(m_Transfrom.position, new Vector3(m_PlayerTransfrom.position.x, 20, m_PlayerTransfrom.position.z), CameraSpeed);
        }
        if (Input.GetMouseButton(0))
        {
            ZhuangTai = 1;
            m_Transfrom.position = Vector3.MoveTowards(m_Transfrom.position, new Vector3(m_PlayerTransfrom.position.x, 25, m_PlayerTransfrom.position.z), CameraSpeed);
        }
        if (Input.GetMouseButtonUp(0) && FireworkScripts.GG != 1)
        {
            ZhuangTai = 0;
        }
    }
}
