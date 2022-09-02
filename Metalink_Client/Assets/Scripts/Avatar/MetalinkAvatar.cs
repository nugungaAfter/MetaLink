using UnityEngine.InputSystem;
using UnityEngine;

namespace Metalink.Avatar
{
    [RequireComponent(typeof(Animator))]
    public class MetalinkAvatar : MetalinkRemoteAvatar
    {
        [SerializeField] private MetalinkGesture gesture;
        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private Transform eyeOffset;

        private CharacterController characterController;

        protected override void Awake()
        {
            base.Awake();

            transform.position = positionOffset;
            gesture?.BindActionAll(animator);

            CreateEyeCam();
        }

        protected override void Update() => base.Update();

        protected override void FixedUpdate() => base.FixedUpdate();

        protected override void LateUpdate() => base.LateUpdate();

        protected override void OnAnimatorIK() => base.OnAnimatorIK();

        protected override void UpdateAnimator()
        {
            base.UpdateAnimator();
            animator.SetBool("IsGrounded", characterController.isGrounded);
        }

        private void CreateEyeCam()
        {
            var head = animator.GetBoneTransform(HumanBodyBones.Head);
            Camera camera = new GameObject("Eye Cam").AddComponent<Camera>();
            camera.depth = 1;
            camera.nearClipPlane = 0.01f;
            camera.farClipPlane = 10000f;

            camera.transform.parent = eyeOffset;
            camera.transform.localPosition = Vector3.zero;
            camera.transform.localRotation = Quaternion.identity;
        }
    }
}