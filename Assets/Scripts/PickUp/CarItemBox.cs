using MyGameApplication.Item;
using MyGameApplication.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.PickUp {
    public class CarItemBox : PickUp {
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
                GameObject itemObj = itemManager.CreateItemObjectById(itemId);
                BaseItem item = null;
                if (itemObj) item = itemObj.GetComponent<BaseItem>();
                if (item && item is Prop && ((Prop)item).isCarItem()) {
                    CarItemBarController.Instance.addProp(itemId);
                    onGain();
                }
                else {
                    m_PickedItemId = itemId;
                    GainItem();
                }
                itemManager.ReleaseItemObjectById(itemId, itemObj);
            }
        }

        protected override void onGain() {
            base.onGain();
            gameObject.SetActive(false);
            Invoke("appear", 5);
        }
        private void appear() {
            gameObject.SetActive(true);
        }
    }
}
