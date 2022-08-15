using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

namespace Metalink.Object
{
    public class Manager_ObjectManager : MonoBehaviour
    {
        public List<MetalinkBehaviour> m_MetalinkBehaviours = new List<MetalinkBehaviour>();

        public UnityAction m_Update;
        public UnityAction m_FixedUpdate;
        public UnityAction m_LateUpdate;

        public void Awake()
        {
            UpdateAsync();
        }

        public void LoadObject()
        {

        }

        public async void UpdateAsync()
        {
            while (true) {
                if (m_Update == null)
                    m_Update.Invoke();
                await Task.Delay(1);
            }
        }
    }
}