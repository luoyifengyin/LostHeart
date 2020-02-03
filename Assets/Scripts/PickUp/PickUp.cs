using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.PickUp {
    public class PickUp : BasePickUp {
        [SerializeField] protected string m_ButtonName;
        [SerializeField] protected KeyCode m_Key;
        [SerializeField] protected string[] m_CanPickByTags = { "Player" };

        private void Awake() {
            m_ButtonName = m_ButtonName.Trim();
        }

        private void CheckPickUp(Collider other) {
            bool touch = false;
            for(int i = 0; i < m_CanPickByTags.Length && !touch; i++) {
                if (other.gameObject.CompareTag(m_CanPickByTags[i]))
                    touch = true;
            }
            if (touch) {
                if ((string.IsNullOrEmpty(m_ButtonName) || CrossPlatformInputManager.GetButtonDown(m_ButtonName))
                        && (m_Key == KeyCode.None || Input.GetKeyDown(m_Key)))
                    PickUpItem();
            }
        }

        private void OnTriggerEnter(Collider other) {
            CheckPickUp(other);
        }
        private void OnCollisionEnter(Collision collision) {
            CheckPickUp(collision.collider);
        }

        protected override void OnPicked() {
            base.OnPicked();
            if (!IsSelfItem) Destroy(gameObject);
        }
    }
}
