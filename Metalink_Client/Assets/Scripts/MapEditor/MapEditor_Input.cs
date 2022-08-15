using UnityEngine.InputSystem;

namespace Metalink.MapEditor
{
    public class MapEditor_Input : MapEditor_InputBase
    {
        public override void Awake()
        {
            base.Awake();

            var I_mapEditorInput = m_PlayerInputAction.MapEditor;

            I_mapEditorInput.Pan.started += val => m_IsPan = true;
            I_mapEditorInput.Pan.canceled += val => m_IsPan = false;

            I_mapEditorInput.Rotate.started += val => m_IsRotate = true;
            I_mapEditorInput.Rotate.canceled += val => m_IsRotate = false;

            I_mapEditorInput.UpDown.performed += UpdateUpDownAxis;
            I_mapEditorInput.UpDown.canceled += UpdateUpDownAxis;
        }

        public void UpdateUpDownAxis(InputAction.CallbackContext p_CallbackContext) => m_UpDownAxis = p_CallbackContext.ReadValue<float>();

        public override void OnEnable() => base.OnEnable();

        public override void OnDisable() => base.OnDisable();
    }
}
