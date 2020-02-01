using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Item {
    public class Prop : BaseItem {
        public virtual bool isCarItem() {
            return false;
        }
    }
}
