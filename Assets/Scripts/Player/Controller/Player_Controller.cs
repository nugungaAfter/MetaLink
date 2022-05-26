using UnityEngine.Events;
using UnityEngine;

namespace Metalink.Player
{
    public class Player_Controller : Player_ControllerBase, IPlayer_ControllerSetter, IPlayer_ControllerUpdate, IPlayer_ControllerAction, IPlayer_ControllerReset
    {
        protected float CurrentSpeed
        {
            get
            {
                float speed = m_PlayerStatus.m_MoveSpeed;

                if (m_PlayerInput.m_IsCrouching)
                    speed *= m_PlayerStatus.m_CrouchMulti;
                else if (m_PlayerInput.m_IsRunning)
                    speed *= m_PlayerStatus.m_RunMulti;

                return speed;
            }
        }

        public override void Reset()
        {
            base.Reset();
            m_PlayerInput = GetComponent<Player_Input>();
            m_PlayerPhysic = GetComponent<Player_Physic>();
        }

        public virtual void Awake()
        {
            m_PlayerInput.m_JumpAction += Jump;
            m_PlayerInput.m_CrouchAction += Crouch;

            SetCollider();
            SetCameraPostion();
        }

        private void Update()
        {
            Move();
            Rotate();
            RotateView();
        }

        public void SetCameraPostion()
        {
            float I_cameraHeight = m_PlayerInput.m_IsCrouching ? m_PlayerStatus.m_CrouchCameraHeight : m_PlayerStatus.m_StandCameraHeight;
            m_FirstPersonCamera.transform.position = transform.position + new Vector3(0, I_cameraHeight, 0);
        }
        
        public void SetCollider()
        {
            float I_colliderHeight = m_PlayerInput.m_IsCrouching ? m_PlayerStatus.m_CrouchColliderHeight : m_PlayerStatus.m_StandColliderHeight;

            m_CapsuleCollider.height = I_colliderHeight;
            m_CapsuleCollider.center = new Vector3(0, I_colliderHeight / 2f, 0);
        }

        public void Move()
        {
            Vector2 I_moveDelta = m_PlayerInput.MoveDelta;
            Vector3 I_moveDIr = new Vector3(I_moveDelta.x, 0, I_moveDelta.y);
            m_Rigidbody.velocity = transform.TransformDirection(CurrentSpeed * I_moveDIr) + new Vector3(0, m_Rigidbody.velocity.y, 0);

            if(m_MoveEvent != null)
                m_MoveEvent();
        }

        public void Rotate()
        {
            if (m_ViewLock)
                return;

            Vector2 I_mouseDelta = m_PlayerInput.MouseDelta;
            m_ViewAngleY += I_mouseDelta.x * m_PlayerStatus.m_CameraHorizontalSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, m_ViewAngleY, 0);

            if(m_RotateEvent != null)
                m_RotateEvent();
        }

        public void RotateView()
        {
            if (m_ViewLock)
                return;

            Vector2 I_mouseDelta = m_PlayerInput.MouseDelta;
            m_ViewAngleX += -I_mouseDelta.y * m_PlayerStatus.m_CameraVerticalSpeed * Time.deltaTime;
            m_ViewAngleX = Mathf.Clamp(m_ViewAngleX, m_PlayerStatus.m_CameraVerticalRange.y, m_PlayerStatus.m_CameraVerticalRange.x);

            m_FirstPersonCamera.transform.localRotation = Quaternion.Euler(m_ViewAngleX, 0, 0);

            if (m_RotateEvent != null)
                m_RotateEvent();
        }

        public void Jump()
        {
            if (!m_PlayerPhysic.IsGround)
                return;

            m_Rigidbody.AddRelativeForce(Vector3.up * m_PlayerStatus.m_JumpSpeed, ForceMode.VelocityChange);

            if (m_JumpEvent != null)
                m_JumpEvent();
        }
        public void Crouch()
        {
            SetCollider();
            SetCameraPostion();
        }

        public void PostionReset()
        {
            throw new System.NotImplementedException();
        }

        public void RotateReset()
        {
            throw new System.NotImplementedException();
        }

        public void ViewReset()
        {
            throw new System.NotImplementedException();
        }
    }
}
