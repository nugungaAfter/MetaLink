using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Metalink.Avatar
{
    [System.Serializable]
    public class MetalinkGestureData
    {
        public InputActionReference bindingAction; // 제스처가 등록될 액션
        public string animatorKeyName; // 애니메이터에서 변경할 bool 값 이름
        public bool isToggle; // 토글 형식의 제스쳐인지

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
