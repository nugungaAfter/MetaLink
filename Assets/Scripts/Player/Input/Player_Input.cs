using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;

namespace Metalink.Player
{
    public class Player_Input : Player_InputBase, IPlayer_InputUpdate
    {
        public void Awake()
        {
            m_PlayerInputAction = new PlayerInputAction();

            m_PlayerInputAction.FirstPerson.Move.performed += UpdateMoveDelta;  
            m_PlayerInputAction.FirstPerson.Move.canceled += UpdateMoveDelta;  

            m_PlayerInputAction.FirstPerson.View.performed += UpdateViewDelta; 
            m_PlayerInputAction.FirstPerson.View.canceled += UpdateViewDelta;
            
            m_PlayerInputAction.FirstPerson.Jump.started += val => m_JumpAction();

            m_PlayerInputAction.FirstPerson.Run.started += val => m_IsRunning = true;
            m_PlayerInputAction.FirstPerson.Run.canceled += val => m_IsRunning = false;

            m_PlayerInputAction.FirstPerson.Crouch.started += val =>
            {
                m_IsCrouching = !m_IsCrouching;
                m_CrouchAction();
            };

            m_PlayerInputAction.FirstPerson.Grab.started += val => m_GrabAction();

            m_PlayerInputAction.FirstPerson.Interaction.started += val => m_InteractionAction();
        }

        public void OnEnable() => m_PlayerInputAction.Enable();

        public void OnDisable() => m_PlayerInputAction.Disable();

        public void UpdateMoveDelta(InputAction.CallbackContext callbackContext) => this.m_MoveDelta = callbackContext.ReadValue<Vector2>();

        public void UpdateViewDelta(InputAction.CallbackContext callbackContext) => this.m_MouseDelta = callbackContext.ReadValue<Vector2>();
    }
}