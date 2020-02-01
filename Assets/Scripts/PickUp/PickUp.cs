using MyGameApplication.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.PickUp {
    public class PickUp : MonoBehaviour {
        [SerializeField] protected int m_PickedItemId;
        [SerializeField] protected bool m_IsItem = false;
        private BaseItem m_ItemController;

        private void Awake() {
            m_ItemController = GetComponent<BaseItem>();
            if (m_ItemController) {
                m_ItemController.ItemId = m_PickedItemId;
            }
        }

        public void GainItem(GameObject obj = null) {
            if (m_IsItem || m_ItemController) {
                PlayerBag.Instance.addItem(m_PickedItemId, 1, gameObject);
            }
            else {
                PlayerBag.Instance.addItem(m_PickedItemId, 1, obj);
                onGain();
            }
        }

        protected virtual void onGain() {
            Destroy(gameObject);
        }
    }
}
