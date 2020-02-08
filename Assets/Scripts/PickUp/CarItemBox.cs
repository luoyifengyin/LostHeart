using MyGameApplication.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.PickUp {
    public class CarItemBox : BasePickUp {
        [SerializeField] private float m_FreshTime = 5;

        // Update is called once per frame
        void Update() {
            //transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
            transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other) {
            GameObject go = other.gameObject;
            if (go.CompareTag("Car")) {
                ItemManager itemManager = ItemManager.Instance;
                int itemId = itemManager.GetRandomCarItemId();
                GameObject itemObj = itemManager.itemList[itemId].prefab;
                BaseItem item = null;
                if (itemObj) item = itemObj.GetComponent<BaseItem>();
                if (item && item is Prop && ((Prop)item).isCarItem()) {
                    CarItemBar.Instance.AddProp(itemId);
                    OnPicked();
                }
                else {
                    PickUpItem(itemId);
                }
            }
        }

        protected override void OnPicked() {
            base.OnPicked();
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
