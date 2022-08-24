using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Matalink.Keybord
{
    public class AnimateHandOnInput : MonoBehaviour
    {
        public InputActionProperty g_pinchAnimationAction;
        public InputActionProperty g_gripAnimationAction;
        public Animator g_handAnimator;

        void Start()
        {
        
        }

        void Update()
        {
            float triggerValue = g_pinchAnimationAction.action.ReadValue<float>();
            g_handAnimator.SetFloat("Trigger", triggerValue);

            float gripValue = g_gripAnimationAction.action.ReadValue<float>();
            g_handAnimator.SetFloat("Grip", gripValue);
        }
    }
}
