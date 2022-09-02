using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metalink.Avatar
{
    public class MetalinkRemoteAvatar : MonoBehaviour
    {
        protected Animator animator;
        protected XRRig xrRig;


        protected Vector3 lastHeadPostion = Vector3.zero;
        protected Vector3 headVelocity => (xrRig.head.position - lastHeadPostion).normalized;

        public XRRig XRRig => xrRig;

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            UpdateAnimator();
            TrackCamera();
        }

        protected virtual void FixedUpdate()
        {
            lastHeadPostion = xrRig.head.position;
        }

        protected virtual void LateUpdate()
        {
            var head = animator.GetBoneTransform(HumanBodyBones.Head);
            head.transform.rotation = this.xrRig.head.rotation;
        }

        protected virtual void OnAnimatorIK()
        {
            if (xrRig.leftHandController != null)
                UpdateIK(AvatarIKGoal.LeftHand, xrRig.leftHandController);

            if (xrRig.rightHandController != null)
                UpdateIK(AvatarIKGoal.RightHand, xrRig.rightHandController);
        }

        protected void TrackCamera()
        {
            Vector3 headPosition = this.xrRig.head.position;
            headPosition.y = transform.position.y;

            transform.position = headPosition;
        }

        protected void UpdateIK(AvatarIKGoal bone, Transform target, AvatarIKHint hint = default, Transform hintTarget = null)
        {
            animator.SetIKPositionWeight(bone, 1);
            animator.SetIKRotationWeight(bone, 1);

            animator.SetIKPosition(bone, target.position);
            animator.SetIKRotation(bone, target.rotation);

            if (hint == default || hintTarget == null)
                return;

            animator.SetIKHintPositionWeight(hint, 1);
            animator.SetIKHintPosition(hint, hintTarget.position);
        }

        protected virtual void UpdateAnimator()
        {
            animator.SetFloat("Velocity X", headVelocity.x);
            animator.SetFloat("Velocity Y", headVelocity.y);
            animator.SetFloat("Velocity Z", headVelocity.z);
        }
    }
}