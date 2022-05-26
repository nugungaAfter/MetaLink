using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metalink.Interaction
{
    public class Interaction_Door : Interaction_Base, Interaction_Object
    {
        public Animator m_Animator;
        public bool m_IsDoorOpen;
        public bool m_IsAuto;

        public void Reset()
        {
            m_Animator = GetComponent<Animator>();
        }

        public void Awake()
        {
            if (m_IsAuto)
                return;

            m_InteractionEvent.AddListener(() =>
            {
                m_IsDoorOpen = !m_IsDoorOpen;
                m_Animator.SetBool("IsDoorOpen", m_IsDoorOpen);
            });
        }

        public void OnTriggerStay(Collider p_Collision) => DoorAuto(p_Collision, true);

        public void OnTriggerExit(Collider p_Collision) => DoorAuto(p_Collision, false);

        public void DoorAuto(Collider p_Collision, bool p_IsOpen)
        {
            if (!m_IsAuto)
                return;

            if (p_Collision.gameObject.CompareTag("Player"))
                m_Animator.SetBool("IsDoorOpen", p_IsOpen);
        }
    }
}