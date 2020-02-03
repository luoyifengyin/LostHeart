using MyGameApplication.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.PickUp {
    public class BasePickUp : MonoBehaviour {
        [SerializeField] protected int m_PickedItemId;          //玩家将会获得的道具id
        [SerializeField] protected int m_PickedCnt = 1;         //玩家将会获得的道具数量
        [SerializeField] protected bool m_IsSelfItem = false;   //该gameObject是否为道具本身
        private BaseItem m_ItemController;

        public bool IsSelfItem {
            get {
                return m_IsSelfItem || m_ItemController;
            }
        }

        private void Awake() {
            m_ItemController = GetComponent<BaseItem>();
            if (m_ItemController) {
                m_ItemController.ItemId = m_PickedItemId;
            }
        }

        //捡起道具
        public void PickUpItem(int id = 0, int? cnt = null) {
            if (id > 0) m_PickedItemId = id;
            m_PickedCnt = cnt ?? m_PickedCnt;
            PlayerBag.Instance.AddItem(m_PickedItemId, m_PickedCnt);
            if (IsSelfItem) ItemManager.Instance.ReleaseItemObjectById(m_PickedItemId, gameObject);
            OnPicked();
        }

        //捡起道具时会触发
        protected virtual void OnPicked() { }
    }
}
