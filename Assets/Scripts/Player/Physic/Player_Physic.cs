using UnityEngine.Events;
using UnityEngine;

namespace Metalink.Player
{
    public class Player_Physic : Player_Base, IPlayer_PhysicUpdate
    {
        private bool m_IsGround = false;
        public UnityAction m_LandEvent;

        /// <summary>
        /// 캡슐 콜라이더 하단의 중심좌표
        /// </summary>
        public Vector3 GroundSphereCenter => new Vector3(transform.position.x, transform.position.y + m_CapsuleCollider.radius, transform.position.z);

        /// <summary>
        /// 캡슐 콜라이더 하단의 중심좌표 + 오프셋
        /// </summary>
        public Vector3 GroundSphereOffset => GroundSphereCenter + (Vector3.up * (m_CapsuleCollider.radius * 0.1f));

        public float SafeRadius => m_CapsuleCollider.radius * 0.95f;

        public int CastMask
        {
            get
            {
                return (1 << LayerMask.NameToLayer("LocalPlayer"));
            }
        }

        public RaycastHit GroundCast
        {
            get
            {
                Physics.SphereCast(GroundSphereOffset, m_CapsuleCollider.radius, Vector3.down, out RaycastHit I_raycastHit, 0.1f);
                return I_raycastHit;
            }
        }

        /// <summary>
        /// 바닥의 표면 각도
        /// </summary>
        public float SurfaceAngle
        {
            get
            {
                var I_angle = Vector3.Angle(Vector3.up, GroundCast.normal) + 0.05f;
                return Mathf.Round(I_angle);
            }
        }

        /// <summary>
        /// 지면에 닿아 있는지 여부
        /// </summary>
        public bool IsGround
        {
            get => m_IsGround;
            set
            {
                if (value == m_IsGround) return;
                m_IsGround = value;
                if (m_IsGround && m_LandEvent != null)
                    m_LandEvent();
            }
        }

        public bool CanCrouchUp
        {
            get
            {
                Physics.SphereCast(transform.position, SafeRadius, Vector3.up, out RaycastHit I_raycastHit, m_CapsuleCollider.height, CastMask);
                return I_raycastHit.transform == null;
            }
        }

        public void Update()
        {
            GroundCheck();
        }

        public void GroundCheck()
        {
            IsGround = GroundCast.collider != null;
        }
    }
}