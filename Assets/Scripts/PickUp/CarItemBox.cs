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
                BaseItem item = itemManager.CreateItemObjectById(itemId).GetComponent<BaseItem>();
                if (item is Prop && ((Prop)item).isCarItem()) {
                    CarItemBarController.Instance.addProp((Prop)item);
                    onGain();
                }
                else {
                    m_PickedItemId = itemId;
                    GainItem(item.gameObject);
                }
            }
        }

        protected override void onGain() {
            gameObject.SetActive(false);
            Invoke("appear", 5);
        }
        private void appear() {
            gameObject.SetActive(true);
        }
    }
}
