using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metalink.Player
{
    public class Player_InteractionBase : MonoBehaviour
    {
        public Player_Input m_PlayerInput;
        public Camera m_FirstPersonCamera;
        public Transform m_GrabPoint;

        [Space(10)]
        public Player_Status m_PlayerStatus;

        protected GameObject m_GrabObject = null;
        protected Transform m_GrabObjectParent = null;

        public bool m_InteractionLock;

        protected RaycastHit InteractionCast
        {
            get
            {
                Ray I_cameraRay = m_FirstPersonCamera.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(I_cameraRay, out RaycastHit I_raycastHit, m_PlayerStatus.m_InteractionDistance);
                return I_raycastHit;
            }
        }

        public void Reset()
        {
            m_FirstPersonCamera = GetComponentInChildren<Camera>();
            m_PlayerInput = GetComponent<Player_Input>();
            m_PlayerStatus = null;
        }
    }
}