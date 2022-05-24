using UnityEngine;

namespace Metalink.Player
{
    public class Player_Base : MonoBehaviour
    {
        [Header("Components")]
        public Rigidbody m_Rigidbody;
        public CapsuleCollider m_CapsuleCollider;
        public Camera m_FirstPersonCamera;
        public Player_Status m_PlayerStatus;

        public virtual void Reset()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_CapsuleCollider = GetComponent<CapsuleCollider>();
            m_FirstPersonCamera = GetComponentInChildren<Camera>();
            m_PlayerStatus = null;
        }
    }
}