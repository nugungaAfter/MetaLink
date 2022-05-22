using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace Metalink.Player
{
    public interface IPlayer_ControllerSetter
    {
        public void SetCameraPostion();

        public void SetCollider();
    }

    public interface IPlayer_ControllerReset
    {
        public void PostionReset();

        public void RotateReset();

        public void ViewReset();
    }

    public interface IPlayer_ControllerUpdate
    {
        public void Move();

        public void Rotate();

        public void RotateView();

        public void GroundCheck();
    }

    public interface IPlayer_ControllerAction
    {
        public void Jump();

        public void Crouch();
    }
    

    public class Player_ControllerBase : MonoBehaviour
    {
        [Header("Components")]
        public Rigidbody m_Rigidbody;
        public CapsuleCollider m_CapsuleCollider;
        public Camera m_FirstPersonCamera;
        public Player_ControllerInput m_PlayerControllerInput;

        [Space(10)]
        public Player_Status m_PlayerStatus;

        public virtual void Reset()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_CapsuleCollider = GetComponent<CapsuleCollider>();
            m_FirstPersonCamera = GetComponentInChildren<Camera>();
            m_PlayerControllerInput = GetComponent<Player_ControllerInput>();

            m_PlayerStatus = null;
        }
    }

    public class Player_ControllerExtension : Player_ControllerBase
    {
        protected float m_ViewAngleX;
        protected float m_ViewAngleY;

        protected float CurrentSpeed
        {
            get
            {
                float speed = m_PlayerStatus.m_MoveSpeed;

                if (m_PlayerControllerInput.m_IsCrouching)
                    speed *= m_PlayerStatus.m_CrouchMulti;
                else if(m_PlayerControllerInput.m_IsRunning)
                    speed *= m_PlayerStatus.m_RunMulti;

                return speed;
            }
        }
    }

    public class Player_ActionEvnet : Player_ControllerExtension
    {
        public UnityAction m_MoveEvent;
        public UnityAction m_JumpEvent;
        public UnityAction m_LandEvent;
        public UnityAction m_RotateEvent;
    }

    public class Player_Physic : Player_ActionEvnet
    {
        private bool m_IsGround = false;

        #region Properties
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
        #endregion
    }
}