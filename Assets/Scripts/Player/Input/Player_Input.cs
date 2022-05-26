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
        public void Awake()
        {
            m_PlayerInputAction = new PlayerInputAction();
            var I_firstPerson = m_PlayerInputAction.FirstPerson;
            var I_base = m_PlayerInputAction.Base;

            I_firstPerson.Move.performed += UpdateMoveDelta;
            I_firstPerson.Move.canceled += UpdateMoveDelta;

            I_firstPerson.View.performed += UpdateViewDelta;
            I_firstPerson.View.canceled += UpdateViewDelta;

            I_firstPerson.Jump.started += val => m_JumpAction();

            I_firstPerson.Run.started += val => m_IsRunning = true;
            I_firstPerson.Run.canceled += val => m_IsRunning = false;

            I_firstPerson.Crouch.started += val =>
            {
                m_IsCrouching = !m_IsCrouching;
                m_CrouchAction();
            };

            I_firstPerson.Grab.started += val => m_GrabAction();

            I_firstPerson.Interaction.started += val => m_InteractionAction();

            I_base.OpenMenu.started += OnClickMenuKey;
        }

        public void OnEnable() => m_PlayerInputAction.Enable();

        public void OnDisable() => m_PlayerInputAction.Disable();

        public void UpdateMoveDelta(InputAction.CallbackContext callbackContext) => this.m_MoveDelta = callbackContext.ReadValue<Vector2>();

        public void UpdateViewDelta(InputAction.CallbackContext callbackContext) => this.m_MouseDelta = callbackContext.ReadValue<Vector2>();

        public void OnClickMenuKey(InputAction.CallbackContext callbackContext)
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