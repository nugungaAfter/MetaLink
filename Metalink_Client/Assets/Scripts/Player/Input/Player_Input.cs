using System.Collections;
using System.Collections.Generic;

using UnityEngine.InputSystem;
using UnityEngine;

using Metalink.Manager;
using Metalink.UI;

namespace Metalink.Player
{
    public class Player_Input : Player_InputBase, IPlayer_InputUpdate
    {
        public virtual void Awake()
        {
            m_PlayerInputAction = new PlayerInputAction();
            var I_firstPerson = m_PlayerInputAction.FirstPerson;
            var I_base = m_PlayerInputAction.Base;

            I_firstPerson.Move.performed += UpdateMoveDelta;
            I_firstPerson.Move.canceled += UpdateMoveDelta;

            I_firstPerson.View.performed += UpdateViewDelta;
            I_firstPerson.View.canceled += UpdateViewDelta;

            I_firstPerson.MousePosition.performed += UpdateMousePostion;
            I_firstPerson.MousePosition.canceled += UpdateMousePostion;

            I_firstPerson.Run.started += val => m_IsRunning = true;
            I_firstPerson.Run.canceled += val => m_IsRunning = false;

            I_firstPerson.Jump.started += val =>
            {
                if (m_JumpAction != null)
                    m_JumpAction();
            };

            I_firstPerson.Crouch.started += val =>
            {
                m_IsCrouching = !m_IsCrouching;
                
                if(m_CrouchAction != null)
                    m_CrouchAction();
            };

            I_firstPerson.Grab.started += val =>
            {
                if (m_GrabAction != null)
                    m_GrabAction();
            };

            I_firstPerson.Interaction.started += val =>
            {
                if (m_InteractionAction != null)
                    m_InteractionAction();
            };
        }

        public virtual void OnEnable() => m_PlayerInputAction.Enable();

        public virtual void OnDisable() => m_PlayerInputAction.Disable();

        public void UpdateMoveDelta(InputAction.CallbackContext p_CallbackContext) => this.MoveDelta = p_CallbackContext.ReadValue<Vector2>();

        public void UpdateViewDelta(InputAction.CallbackContext p_CallbackContext) => this.MouseDelta = p_CallbackContext.ReadValue<Vector2>();

        public void UpdateMousePostion(InputAction.CallbackContext p_CallbackContext) => this.m_MousePosition = p_CallbackContext.ReadValue<Vector2>();
    }
}