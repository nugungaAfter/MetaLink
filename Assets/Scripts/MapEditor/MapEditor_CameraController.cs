using UnityEngine;

namespace Metalink.MapEditor
{
    [RequireComponent(typeof(Camera))]
    public class MapEditor_CameraController : MonoBehaviour
    {
        [SerializeField] private MapEditor_Input m_MapEditorInput;

        [Header("Setting")]
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float panSpeed;

        private Camera m_Camera;
        private Vector2 m_CurrentAxis;

        void Awake()
        {
            m_Camera = GetComponent<Camera>();
        }

        void Update()
        {
            Pan();
            Move();
            Rotate();
        }

        public void Move()
        {
            Vector3 I_MoveDir = new Vector3(m_MapEditorInput.MoveDelta.x, 0, m_MapEditorInput.MoveDelta.y);
            Vector3 I_convertDirection = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * I_MoveDir;
            I_convertDirection.y = m_MapEditorInput.m_UpDownAxis;

            transform.Translate(moveSpeed * Time.deltaTime * I_convertDirection, Space.World);
        }

        public void Pan()
        {
            if (!m_MapEditorInput.m_IsPan)
                return;
            
            transform.Translate(m_MapEditorInput.MouseDelta * Time.deltaTime * panSpeed);
        }

        public void Rotate()
        {
            if (!m_MapEditorInput.m_IsRotate)
                return;

            float I_xDelta = m_MapEditorInput.MouseDelta.x;
            float I_yDelta = m_MapEditorInput.MouseDelta.y;

            m_CurrentAxis += rotateSpeed * Time.deltaTime * new Vector2(-I_yDelta, I_xDelta);
            transform.rotation = Quaternion.Euler(m_CurrentAxis);
        }
    }
}