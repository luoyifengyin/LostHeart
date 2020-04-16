using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Item {
    [Serializable]
    public class Prop : BaseItem {
        public new ItemType type = ItemType.Prop;
        public bool consumable = true;        //是否为消耗品

        public bool isCarItem = false;

        public PropEffect effect;
    }
}
