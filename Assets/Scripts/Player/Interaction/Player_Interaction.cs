using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Metalink.Interaction;

namespace Metalink.Player
{
    public class Player_Interaction : Player_InteractionBase, IPlayer_InterationAction
    {
        void Awake()
        {
            m_PlayerInput.m_GrabAction += GrabObject;
            m_PlayerInput.m_InteractionAction += InteractionObject;

            m_GrabObject = null;
        }

        public void GrabObject()
        {
            if (m_InteractionLock)
                return;

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
            if (m_InteractionLock)
                return;

            Collider I_targetCollider = InteractionCast.collider;

            if (I_targetCollider == null)
                return;

            Interaction_Object I_interactionObject = I_targetCollider.GetComponent<Interaction_Object>();

            if (I_interactionObject == null)
                I_interactionObject = I_targetCollider.GetComponentInChildren<Interaction_Object>();

            if (I_interactionObject == null)
                I_interactionObject = I_targetCollider.GetComponentInParent<Interaction_Object>();

            if (I_interactionObject != null)
                I_interactionObject.Interaction();
        }
    }
}