using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern
{
    public class DesignPattern_Singleton<T> : MonoBehaviour
    {
        private static T m_Instance;

        protected virtual void Awake() { }
        protected virtual void Awake(T p_Instance)
        {
            if(m_Instance == null)
            {
                m_Instance = p_Instance;
                Transform l_parent = transform;
                while(l_parent.parent != null)
                    l_parent = l_parent.parent;
                DontDestroyOnLoad(l_parent);
            }
            else
            {
                Destroy(this);
            }
        }

        public static T Instance
        {
            get => m_Instance;
            protected set => m_Instance = value;
        }
    }
}