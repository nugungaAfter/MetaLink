using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metalink.Player
{
    public class Player_FirstPersonController : Player_Physic, IPlayer_ControllerSetter, IPlayer_ControllerUpdate, IPlayer_ControllerAction, IPlayer_ControllerReset
    {
        public override void Reset() => base.Reset();

        public void Awake()
        {
            m_PlayerControllerInput.m_JumpAction += Jump;
            m_PlayerControllerInput.m_CrouchAction += Crouch;

            SetCollider();
            SetCameraPostion();
        }

        private void Update()
        {
            Move();
            Rotate();
            RotateView();
            GroundCheck();
        }

        public void SetCameraPostion()
        {
            float I_cameraHeight = m_PlayerControllerInput.m_IsCrouching ? m_PlayerStatus.m_CrouchCameraHeight : m_PlayerStatus.m_StandCameraHeight;
            m_FirstPersonCamera.transform.position = transform.position + new Vector3(0, I_cameraHeight, 0);
        }
        
        public void SetCollider()
        {
            float I_colliderHeight = m_PlayerControllerInput.m_IsCrouching ? m_PlayerStatus.m_CrouchColliderHeight : m_PlayerStatus.m_StandColliderHeight;

            m_CapsuleCollider.height = I_colliderHeight;
            m_CapsuleCollider.center = new Vector3(0, I_colliderHeight / 2f, 0);
        }

        public void Move()
        {
            Vector2 I_moveDelta = m_PlayerControllerInput.MoveDelta;
            Vector3 I_moveDIr = new Vector3(I_moveDelta.x, 0, I_moveDelta.y);
            m_Rigidbody.velocity = transform.TransformDirection(CurrentSpeed * I_moveDIr) + new Vector3(0, m_Rigidbody.velocity.y, 0);

            if(m_MoveEvent != null)
                m_MoveEvent();
        }

        public void Rotate()
        {
            Vector2 I_mouseDelta = m_PlayerControllerInput.MouseDelta;
            m_ViewAngleY += I_mouseDelta.x * m_PlayerStatus.m_CameraHorizontalSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, m_ViewAngleY, 0);

            if(m_RotateEvent != null)
                m_RotateEvent();
        }

        public void RotateView()
        {
            Vector2 I_mouseDelta = m_PlayerControllerInput.MouseDelta;
            m_ViewAngleX += -I_mouseDelta.y * m_PlayerStatus.m_CameraVerticalSpeed * Time.deltaTime;
            m_ViewAngleX = Mathf.Clamp(m_ViewAngleX, m_PlayerStatus.m_CameraVerticalRange.y, m_PlayerStatus.m_CameraVerticalRange.x);

            m_FirstPersonCamera.transform.localRotation = Quaternion.Euler(m_ViewAngleX, 0, 0);

            if (m_RotateEvent != null)
                m_RotateEvent();
        }

        public void GroundCheck()
        {
            IsGround = GroundCast.collider != null;
        }

        public void Jump()
        {
            if (!IsGround)
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
