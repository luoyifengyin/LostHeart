using MyGameApplication.Car;
using MyGameApplication.Item;
using MyGameApplication.Item.Inventory;
using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.PickUp {
    public class CarItemBox : BasePickUp {
        [SerializeField] private float m_FreshTime = 5;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Car")) {
                if (!other.gameObject.GetGameObjectInParentWithTag("Player")) {
                    OnPicked(0);
                    return;
                }
                ItemManager itemManager = ItemManager.Instance;
                int itemId = itemManager.GetRandomCarItemId();

                if (itemManager.propList[itemId].isCarItem)
                    m_Inventory = CarItemBar.Instance;
                else m_Inventory = PlayerBag.Instance;

                PickUpItem(itemId);
            }
        }

        protected override void OnPicked(int pickedCnt = 1, int overflowCnt = 0) {
            base.OnPicked(pickedCnt, overflowCnt);
            gameObject.SetActive(false);
            Invoke("Appear", m_FreshTime);
        }
        private void Appear() {
            gameObject.SetActive(true);
        }
    }
}
