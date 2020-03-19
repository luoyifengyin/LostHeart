using MyGameApplication.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.PickUp {
    public class PickUp : BasePickUp {
        private const string PICK_UP = "PickUp";
        [SerializeField] protected bool m_AutoPickUp = true;
        [SerializeField] protected bool m_WhetherPickIfOverflow;
        [SerializeField] protected string[] m_CanPickByTags = { "Player" };

        private void CheckPickUp(Collider other) {
            bool touch = false;
            for(int i = 0; i < m_CanPickByTags.Length && !touch; i++) {
                if (other.gameObject.CompareTag(m_CanPickByTags[i]))
                    touch = true;
            }
            if (touch) {
                if ((m_AutoPickUp || CrossPlatformInputManager.GetButtonDown(PICK_UP))) {
                    if (!PlayerBag.Instance.IsFullOfItemById(m_PickedItemId) || m_WhetherPickIfOverflow)
                        PickUpItem();
                    else OnNonPicked();
                }
            }
        }

        private void OnTriggerEnter(Collider other) {
            CheckPickUp(other);
        }
        private void OnCollisionEnter(Collision collision) {
            CheckPickUp(collision.collider);
        }

        protected override void OnPicked(int pickedCnt, int overflowCnt) {
            base.OnPicked(pickedCnt, overflowCnt);
            if (!IsSelfItem) Destroy(gameObject);
        }

        protected virtual void OnNonPicked() { }
    }
}
