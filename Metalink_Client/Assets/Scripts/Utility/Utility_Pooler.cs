using System.Collections.Generic;
using System.Collections;
using UnityEngine.Pool;
using UnityEngine;
using Metalink.Interaction;

namespace Metalink.Utility
{
    public interface IPoolObject
    {
        public GameObject GameObject { get; }

        public void SetManagedPool(IObjectPool<IPoolObject> p_Pooler);

        public void DestoryObject();
    }

    public class Utility_Pooler : MonoBehaviour
    {
        [SerializeField]
        protected GameObject m_PoolObject;
        protected IObjectPool<IPoolObject> m_Pool;

        public virtual void SetPool()
        {
            m_Pool = new ObjectPool<IPoolObject>(CreateObject, OnGetObject, OnReleseObject, maxSize: 50);
        }

        public virtual IObjectPool<IPoolObject> GetPool() => m_Pool;

        protected virtual IPoolObject CreateObject()
        {
            IPoolObject l_object = GameObject.Instantiate(m_PoolObject).GetComponent<IPoolObject>();
            l_object.SetManagedPool(m_Pool);
            return l_object;
        }

        protected virtual void OnGetObject(IPoolObject p_PoolObject) => p_PoolObject.GameObject.SetActive(true);

        protected virtual void OnReleseObject(IPoolObject p_PoolObject) => p_PoolObject.GameObject.SetActive(false);
    }
}