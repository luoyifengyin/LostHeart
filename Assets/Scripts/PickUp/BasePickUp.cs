using MyGameApplication.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.PickUp {
    public abstract class BasePickUp : MonoBehaviour {
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
            if (id <= 0) id = m_PickedItemId;
            int count = cnt ?? m_PickedCnt;
            int pickedCnt = PlayerBag.Instance.AddItem(id, count);
            if (IsSelfItem) ItemManager.Instance.ReleaseItemObjectById(id, gameObject);
            OnPicked(pickedCnt, count - pickedCnt);
        }

        //捡起道具时会触发
        protected virtual void OnPicked(int pickedCnt, int overflowCnt = 0) { }
    }
}
