using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication {
    public class CharacterMove : MonoBehaviour {
        private Animator animator;

        public float MoveSpeed = 1f;
        public float SpinSpeed = 50f;
        float SpinSpeedSum = 0;
        // Start is called before the first frame update
        void Start() {
            this.transform.Rotate(new Vector3(0, -90, 0));
        }

        // Update is called once per frame
        void Update() {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            SpinSpeedSum += h * Time.deltaTime * SpinSpeed;
            transform.eulerAngles = new Vector3(0, SpinSpeedSum, 0);
            transform.Translate(Vector3.forward * v * Time.deltaTime * MoveSpeed);

            bool Run = Input.GetButton("Fire1");//Ctrl键

            if (Run == true) {
                MoveSpeed = 2.0f;
            }
            else {
                MoveSpeed = 1.0f;
            }
        }
    }
}
