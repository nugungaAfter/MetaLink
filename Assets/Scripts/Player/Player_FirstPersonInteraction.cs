using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metalink.Player
{
    public interface IPlayer_InterationAction
    {
        public void GrabObject();

        public void CatchGrapObject();

        public void ReleaseGrapObject();

        public void InteractionObject();
    }

    public class Player_FirstPersonInteractionBase : MonoBehaviour
    {
        public Player_ControllerInput m_PlayerControllerInput;
        public Camera m_FirstPersonCamera;
        public Transform m_GrabPoint;

        [Space(10)]
        public Player_Status m_PlayerStatus;

        public void Reset()
        {
            m_FirstPersonCamera = GetComponentInChildren<Camera>();
            m_PlayerControllerInput = GetComponent<Player_ControllerInput>();
            m_PlayerStatus = null;
        }
    }

    public class Player_FirstPersonInteractionExtension : Player_FirstPersonInteractionBase
    {
        protected GameObject m_GrabObject = null;
        protected Transform m_GrabObjectParent = null;

        protected RaycastHit InteractionCast
        {
            get
            {
                Ray I_cameraRay = m_FirstPersonCamera.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(I_cameraRay, out RaycastHit I_raycastHit, m_PlayerStatus.m_InteractionDistance);

                print(I_raycastHit.collider);
                return I_raycastHit;
            }
        }
    }

    public class Player_FirstPersonInteraction : Player_FirstPersonInteractionExtension, IPlayer_InterationAction
    {
        void Awake()
        {
            m_PlayerControllerInput.m_GrabAction += GrabObject;
            m_PlayerControllerInput.m_InteractionAction += InteractionObject;

            m_GrabObject = null;
        }

        public void GrabObject()
        {
            if (m_GrabObject != null)
            {
                ReleaseGrapObject();
                return;
            }

            CatchGrapObject();
        }

        public void CatchGrapObject()
        {
            Collider I_targetCollider = InteractionCast.collider;

            if (I_targetCollider == null)
                return;

            Rigidbody I_targetRigidbody = I_targetCollider.GetComponent<Rigidbody>();

            if (I_targetRigidbody == null)
                return;

            I_targetCollider.enabled = false;
            I_targetRigidbody.isKinematic = true;

            m_GrabObject = I_targetCollider.gameObject;
            m_GrabObjectParent = m_GrabObject.transform.parent;
            m_GrabObject.transform.parent = m_GrabPoint.transform;
            m_GrabObject.transform.position = m_GrabPoint.position;
        }

        public void ReleaseGrapObject() 
        {
            m_GrabObject.GetComponent<Rigidbody>().isKinematic = false;
            m_GrabObject.GetComponent<Collider>().enabled = true;
            m_GrabObject.transform.parent = m_GrabObjectParent;
            m_GrabObject = null;
        }

        public void InteractionObject()
        {

        }
    }
}