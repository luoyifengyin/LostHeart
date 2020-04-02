using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGameApplication.Mainmenu
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float m_RotateSpeed = 1.5f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.transform.Rotate(new Vector3(0, m_RotateSpeed, 0) * Time.deltaTime);
        }
    }
}