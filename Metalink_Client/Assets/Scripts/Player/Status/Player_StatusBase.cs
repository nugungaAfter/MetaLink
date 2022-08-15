using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metalink.Player
{
    [System.Serializable]
    public class Player_StatusBase : ScriptableObject
    {
        [Header("Action")]
        public float m_MoveSpeed;
        public float m_RunMulti;
        public float m_CrouchMulti;
        public float m_JumpSpeed;

        [Header("Height")]
        public float m_StandCameraHeight;
        public float m_CrouchCameraHeight;

        [Space(5)]
        public float m_StandColliderHeight;
        public float m_CrouchColliderHeight;
    }

    [System.Serializable]
    public class Player_StatusInteraction : Player_StatusBase
    {
        [Header("Interaion")]
        public float m_InteractionDistance;
    }
}