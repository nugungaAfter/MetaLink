using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Pool;
using UnityEngine;
using Metalink.Utility;

namespace Metalink.Interaction
{
    public class Interaction_CopyObject : XRGrabInteractable, IPoolObject
    {
        [SerializeField] private float m_DestoryDelay;
        [SerializeField] private bool m_destoryOnCollision;

        private Interaction_CopyGenerate m_Generator;
        private IObjectPool<IPoolObject> m_Pooler;
        private Rigidbody m_Rigidbody;
        private Collider m_Collider;

        //인터페이스 참조용
        public GameObject GameObject => gameObject;

        protected override void Awake()
        {
            base.Awake();

            m_Rigidbody = GetComponent<Rigidbody>();
            m_Collider = GetComponent<Collider>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            m_Collider.isTrigger = false;
            m_Rigidbody.isKinematic = true;
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
        }

        public void SetManagedPool(IObjectPool<IPoolObject> p_Pooler) => m_Pooler = p_Pooler;

        public void SetGenerator(Interaction_CopyGenerate p_Generator) => m_Generator = p_Generator;

        public void DestoryObject()
        {
            CancelInvoke("DestoryObject");
            m_Pooler.Release(this);
        }

        protected override void Grab()
        {
            m_Rigidbody.isKinematic = false;
            transform.parent = null;
            base.Grab();
        }

        protected override void Drop()
        {
            base.Drop();
            m_Collider.isTrigger = true;
            m_Generator.Get();

            Invoke("DestoryObject", m_DestoryDelay);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!m_destoryOnCollision)
                return;

            DestoryObject();
        }
    }
}
