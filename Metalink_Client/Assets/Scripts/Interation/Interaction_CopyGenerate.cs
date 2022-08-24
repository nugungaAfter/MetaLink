using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using Metalink.Utility;
using UnityEngine.Pool;

namespace Metalink.Interaction
{
    public class Interaction_CopyGenerate : Utility_Pooler
    {
        private void Awake()
        {
            SetPool();
            Get();
        }

        public void Get()
        {
            var l_object = m_Pool.Get();
            l_object.GameObject.transform.parent = transform;
        }

        protected override IPoolObject CreateObject()
        {
            var l_object = base.CreateObject();
            l_object.GameObject.GetComponent<Interaction_CopyObject>().SetGenerator(this);
            l_object.GameObject.transform.position = transform.position;
            l_object.GameObject.transform.parent = transform;
            return l_object;
        }
    }
}
