using UnityEngine.Events;

namespace Metalink.Player
{
    public class Player_ControllerBase : Player_Base
    {
        public Player_Input m_PlayerInput;
        public Player_Physic m_PlayerPhysic;

        public UnityAction m_MoveEvent;
        public UnityAction m_RotateEvent;
        public UnityAction m_JumpEvent;

        protected float m_ViewAngleX;
        protected float m_ViewAngleY;

        public bool m_ViewLock;
    }
}