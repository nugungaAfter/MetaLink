using System.Collections.Generic;
using UnityEngine;

namespace Metalink.Object
{
    public abstract class MetalinkComponent : MetalinkObject
    {
        public Transform Transform { get; }
        public GameObject GameObject { get; }

        /// <summary>
        /// 메타링크 커스텀 스크립트
        /// </summary>
        protected Dictionary<string, string> Scripts { get; private set; }

        public MetalinkComponent(Transform p_Transform, GameObject p_GameObject, Dictionary<string, string> p_Scripts) : base()
        {
            Scripts = p_Scripts;
        }

        public T GetComponent<T>()
        {
            if (GameObject.TryGetComponent<T>(out T component))
                return component;
            throw new System.NullReferenceException("Component does not exist");
        }

        public T GetComponentChilderen<T>()
        {
            foreach (Transform child in Transform) {
                if (child.TryGetComponent<T>(out T component)) {
                    return component;
                }
            }
            throw new System.NullReferenceException("Component does not exist");
        }

        public string GetScript(string p_ScriptName) => Scripts[p_ScriptName];

        public void AddScript(string p_ScriptName, string p_Code) => Scripts.Add(p_ScriptName, p_Code);
    }
}