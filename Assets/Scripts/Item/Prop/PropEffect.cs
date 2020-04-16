using UnityEngine;

namespace MyGameApplication.Item {
    public abstract class PropEffect : MonoBehaviour {
        //使用条件
        public virtual bool Condition() {
            return true;
        }

        //道具效果
        public abstract void Operation();

        //道具效果失效时调用
        public virtual void Expire() {}

        //中断该道具的所有效果和特效
        public virtual void Stop() {
            Expire();
        }

        //使用道具
        public void Payload() {
            if (Condition()) {
                Operation();
            }
        }
    }
}
