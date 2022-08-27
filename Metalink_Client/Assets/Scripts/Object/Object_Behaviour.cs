using System.Collections.Generic;
using UnityEngine;

namespace Metalink.Object
{
    public class MetalinkBehaviour : MetalinkComponent
    {
        public MetalinkBehaviour(Transform p_Transform, GameObject p_GameObject, Dictionary<string, string> p_Scripts) : base(p_Transform, p_GameObject, p_Scripts) 
        {
            
        }

        public bool enabled { get; set; }

        public void print(string message) => Debug.Log(message);
        public virtual void Awake() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
        public virtual void OnDestory() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }
        public virtual void OnTriggerEnter(Collider p_Collider) { }
        public virtual void OnTriggerStay(Collider p_Collider) { }
        public virtual void OnTriggerExit(Collider p_Collider) { }
        public virtual void OnCollisionEnter(Collision p_Collision) { }
        public virtual void OnCollisionStay(Collision p_Collision) { }
        public virtual void OnCollisionExit(Collision p_Collision) { }
    }
}
