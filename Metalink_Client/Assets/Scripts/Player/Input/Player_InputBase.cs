using UnityEngine.Events;
using UnityEngine;

namespace Metalink.Player
{
    public class Player_InputBase : MonoBehaviour
    {
        protected PlayerInputAction m_PlayerInputAction;

        protected Vector2 m_MoveDelta;
        protected Vector2 m_MouseDelta;
        protected Vector2 m_MousePosition;

        public Vector2 MoveDelta
        {
            get => m_MoveDelta;
            protected set
            {
                if(m_MoveDelta != value) {
                    m_MoveDelta = value;

                    if (m_MoveDeltaUpdateAction != null)
                        m_MoveDeltaUpdateAction(m_MoveDelta);
                }
            }
        }
        public Vector2 MouseDelta
        {
            get => m_MouseDelta;
            protected set
            {
                if (m_MouseDelta != value) {
                    m_MouseDelta = value;
                    
                    if(m_MouseDeltaUpdateAction != null)
                        m_MouseDeltaUpdateAction(m_MouseDelta);
                }
            }
        }
        public Vector2 MousePosition => m_MousePosition;

        public UnityAction<Vector2> m_MoveDeltaUpdateAction;
        public UnityAction<Vector2> m_MouseDeltaUpdateAction;

        public UnityAction m_JumpAction;
        public UnityAction m_CrouchAction;
        public UnityAction m_GrabAction;
        public UnityAction m_InteractionAction;

        [HideInInspector] public bool m_IsRunning;
        [HideInInspector] public bool m_IsCrouching;
    }
}
