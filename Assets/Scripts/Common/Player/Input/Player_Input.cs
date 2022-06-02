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

            I_base.OpenMenu.started += OnClickMenuKey;
        }

        public virtual void OnEnable() => m_PlayerInputAction.Enable();

        public virtual void OnDisable() => m_PlayerInputAction.Disable();

        public void UpdateMoveDelta(InputAction.CallbackContext p_CallbackContext) => this.m_MoveDelta = p_CallbackContext.ReadValue<Vector2>();

        public void UpdateViewDelta(InputAction.CallbackContext p_CallbackContext) => this.m_MouseDelta = p_CallbackContext.ReadValue<Vector2>();

        public void OnClickMenuKey(InputAction.CallbackContext p_CallbackContext)
        {
            UI_Menu I_menu = Manager_UIManager.g_Instance.GetUI<UI_Menu>();
            I_menu.SetActive(!I_menu.m_activeSelf);

            Manager_GameManager I_gameManager = Manager_GameManager.g_Instance;

            I_gameManager.CursorActive(I_menu.m_activeSelf);
            I_gameManager.m_FirstPersonController.m_ViewLock = I_menu.m_activeSelf;
            I_gameManager.m_FirstPersonController.GetComponent<Player_Interaction>().m_InteractionLock = I_menu.m_activeSelf;
        }
    }
}