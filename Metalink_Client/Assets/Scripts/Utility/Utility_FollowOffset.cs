using UnityEngine;

namespace Metalink.Utility
{
    public class Utility_FollowOffset : MonoBehaviour
    {
        [SerializeField] private Transform m_FollowTarget;
        [SerializeField] private Vector3 m_FollowOffset;

        private void FixedUpdate()
        {
            transform.position = m_FollowTarget.position + m_FollowTarget.TransformDirection(new Vector3(m_FollowOffset.x, 0, m_FollowOffset.z)) + new Vector3(0, m_FollowOffset.y, 0);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(m_FollowTarget.position + m_FollowOffset, 0.1f);
            transform.position = m_FollowTarget.position + m_FollowOffset;
        }
    }
}
