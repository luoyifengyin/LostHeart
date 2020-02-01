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

        public void GainItem(int cnt = 1) {
            PlayerBag.Instance.addItem(m_PickedItemId, cnt);
            onGain();
        }

        protected virtual void onGain() {
            if (m_IsItem || m_ItemController)
                ItemManager.Instance.ReleaseItemObjectById(m_PickedItemId, gameObject);
        }
    }
}
