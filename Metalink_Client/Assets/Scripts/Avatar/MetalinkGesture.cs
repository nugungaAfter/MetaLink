using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Metalink.Avatar
{
    [System.Serializable]
    public class MetalinkGestureData
    {
        public InputActionReference bindingAction; // ����ó�� ��ϵ� �׼�
        public string animatorKeyName; // �ִϸ����Ϳ��� ������ bool �� �̸�
        public bool isToggle; // ��� ������ ����������

        private Animator animator;
        private bool isGestureActive = false;

        public void BindingAction(Animator animator)
        {
            this.animator = animator;

            bindingAction.action.started += val => GestureAction(true);
            bindingAction.action.canceled += val => GestureAction(false);
        }

        public void GestureAction(bool isActive)
        {
            if (isToggle) {
                ToggleGesture(!isGestureActive);
                return;
            }

            ToggleGesture(isActive);
        }

        public void ToggleGesture(bool gestureActive)
        {
            animator.SetBool(animatorKeyName, gestureActive);
            isGestureActive = gestureActive;
            return;
        }
    }

    [CreateAssetMenu(fileName = "New Gesture", menuName = "Create Gesture", order = 19999)]
    public class MetalinkGesture : ScriptableObject
    {
        public List<MetalinkGestureData> gestures;

        public void BindActionAll(Animator animator)
        {
            foreach (var gesture in gestures) {
                gesture.BindingAction(animator);
            }
        }
    }
}
