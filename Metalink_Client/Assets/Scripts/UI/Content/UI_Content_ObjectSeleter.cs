using UnityEngine.UI;
using UnityEngine;
using TMPro;

using Metalink.Object;

namespace Metalink.UI.Content
{
    /// <summary>
    /// 오브젝트 선택기
    /// </summary>
    public class UI_ContentUploader_ObjectSeleter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_ObjectNameText;
        [SerializeField] private Toggle m_IsUploadToggle;

        public void SetUI(UploadObject p_UploadObject)
        {
            m_IsUploadToggle.isOn = p_UploadObject.m_IsUpload;
            m_IsUploadToggle.onValueChanged.AddListener((val) => p_UploadObject.m_IsUpload = val);
            m_ObjectNameText.text = p_UploadObject.m_Object.name;
        }
    }
}