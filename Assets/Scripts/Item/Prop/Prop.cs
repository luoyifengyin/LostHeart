using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Item {
    [Serializable]
    public class Prop : BaseItem {
        public bool consumable = true;        //是否为消耗品

        public bool isCarItem = false;

        public PropEffect effect;

        public Prop() {
            type = ItemType.Prop;
        }
    }
}
