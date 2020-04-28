using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetManMove : MonoBehaviour {
    public float MoveSpeed = 0.1f;      //角色移动速度
    private float h;
    private float v;
    private Vector3 Direction;
    public GameObject WinText;
    public GameObject LeaveButton;
    public GameObject LoseText;
    private void Update()
    {
        PlayerMove();
    }
 
    void PlayerMove()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Direction = new Vector3(h, 0, v);
        transform.LookAt(transform.position + Direction);                 //角色朝向
        transform.Translate(Direction * MoveSpeed, Space.World);          //用Tranlate方法实现移动
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.collider.name == "Enemy")
        {
            LoseText.gameObject.SetActive(true);
            LeaveButton.gameObject.SetActive(true);
            MoveSpeed = 0;
        }
        if (other.collider.name == "LadyFairy")
        {
            WinText.gameObject.SetActive(true);
            LeaveButton.gameObject.SetActive(true);
            MoveSpeed = 0;
        }
    }
}
