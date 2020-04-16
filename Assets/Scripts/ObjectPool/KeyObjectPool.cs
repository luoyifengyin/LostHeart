using System.Collections.Generic;

namespace MyGameApplication.ObjectPool {
    public class KeyObjectPool<TKey, TObject> {
        private Dictionary<TKey, ObjectPool<TObject>> m_ObjectPools = new Dictionary<TKey, ObjectPool<TObject>>();

        private ObjectPool<TObject> GetPool(TKey key) {
            if (!m_ObjectPools.ContainsKey(key)) m_ObjectPools.Add(key, new ObjectPool<TObject>());
            return m_ObjectPools[key];
        }

        public TObject Get(TKey key) {
            var pool = GetPool(key);
            return pool.Get();
        }

        public void Put(TKey key, TObject obj) {
            var pool = GetPool(key);
            pool.Put(obj);
        }
    }
}
