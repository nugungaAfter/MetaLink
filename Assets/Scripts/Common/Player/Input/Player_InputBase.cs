using UnityEngine.Events;
using UnityEngine;

namespace Metalink.Player
{
    public class Player_InputBase : MonoBehaviour
    {
        protected PlayerInputAction m_PlayerInputAction;

        protected Vector2 m_MoveDelta;
        protected Vector2 m_MouseDelta;

        public Vector2 MoveDelta => m_MoveDelta;
        public Vector2 MouseDelta => m_MouseDelta;

        public UnityAction m_JumpAction;
        public UnityAction m_CrouchAction;
        public UnityAction m_GrabAction;
        public UnityAction m_InteractionAction;

        [HideInInspector] public bool m_IsRunning;
        [HideInInspector] public bool m_IsCrouching;
    }
}
