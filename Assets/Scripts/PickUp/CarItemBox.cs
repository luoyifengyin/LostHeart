using MyGameApplication.Car;
using MyGameApplication.Item;
using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.PickUp {
    public class CarItemBox : BasePickUp {
        [SerializeField] private float m_FreshTime = 5;

        private void OnTriggerEnter(Collider other) {
            GameObject go = other.gameObject;
            if (go.CompareTag("Car")) {
                if (!go.transform.FindGameObjectInParentWithTag("Player")) {
                    OnPicked();
                    return;
                }
                ItemManager itemManager = ItemManager.Instance;
                int itemId = itemManager.GetRandomCarItemId();
                var itemEffect = itemManager.itemList[itemId].effect;
                if (itemEffect && itemEffect is Prop && ((Prop)itemEffect).isCarItem()) {
                    CarItemBar.Instance.AddProp(itemId);
                    OnPicked(1);
                }
                else {
                    PickUpItem(itemId);
                }
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

        private void OnMouseOver() {
            print("coin");
        }
    }
}
