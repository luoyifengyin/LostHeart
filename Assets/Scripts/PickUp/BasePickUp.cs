using MyGameApplication.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.PickUp {
    public abstract class BasePickUp : MonoBehaviour {
        [SerializeField] protected ItemType m_PickedItemType = ItemType.Prop;//玩家将会获得的道具类型
        [SerializeField] protected int m_PickedItemId;          //玩家将会获得的道具id
        [SerializeField] protected int m_PickedCnt = 1;         //玩家将会获得的道具数量

        //捡起道具
        public void PickUpItem(int id = 0, ItemType? type = null, int? cnt = null) {
            if (id <= 0) id = m_PickedItemId;
            ItemType itemType = type ?? m_PickedItemType;
            int count = cnt ?? m_PickedCnt;
            int pickedCnt = PlayerBag.Instance.AddItem(id, itemType, count);
            OnPicked(pickedCnt, count - pickedCnt);
        }

        //捡起道具时会触发
        protected virtual void OnPicked(int pickedCnt, int overflowCnt = 0) { }
    }
}
