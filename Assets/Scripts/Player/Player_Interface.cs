using UnityEngine.InputSystem;

namespace Metalink.Player
{
    #region Controller
    public interface IPlayer_ControllerSetter
    {
        public void SetCameraPostion();

        public void SetCollider();
    }

    public interface IPlayer_ControllerReset
    {
        public void PostionReset();

        public void RotateReset();

        public void ViewReset();
    }

    public interface IPlayer_ControllerUpdate
    {
        public void Move();

        public void Rotate();

        public void RotateView();
    }

    public interface IPlayer_ControllerAction
    {
        public void Jump();

        public void Crouch();
    }
    #endregion

    #region Input
    public interface IPlayer_InputUpdate
    {
        public void UpdateMoveDelta(InputAction.CallbackContext callbackContext);

        public void UpdateViewDelta(InputAction.CallbackContext callbackContext);
    }
    #endregion

    #region Physic
    public interface IPlayer_PhysicUpdate
    {
        public void GroundCheck();
    }
    #endregion

    #region Interaction
    public interface IPlayer_InterationAction
    {
        public void GrabObject();

        public void CatchGrapObject();

        public void ReleaseGrapObject();

        public void InteractionObject();
    }
    #endregion
}