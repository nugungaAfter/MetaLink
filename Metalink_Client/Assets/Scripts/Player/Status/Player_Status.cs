using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Metalink.Player
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Player Status", menuName = "Create Player Status Setting", order = int.MaxValue)]
    public class Player_Status : Player_StatusInteraction
    {
        [Header("Camera")]
        public float m_CameraVerticalSpeed;
        public float m_CameraHorizontalSpeed;

        /// <summary>
        /// X = Max, Y = Min
        /// </summary>
        public Vector2 m_CameraVerticalRange;
    }
}
