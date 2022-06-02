using Metalink.Player;
using UnityEngine;

namespace Metalink.MapEditor
{
    public class MapEditor_InputBase : Player_Input
    {
        [HideInInspector] public bool m_IsPan;
        [HideInInspector] public bool m_IsRotate;
        [HideInInspector] public float m_UpDownAxis;
    }
}

