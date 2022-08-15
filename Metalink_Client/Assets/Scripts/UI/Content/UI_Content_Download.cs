using System.Collections.Generic;
using System.Collections;
using UnityEngine;

using TMPro;

using Metalink.Object;

namespace Metalink.UI.Content
{
    public class UI_Content_Download : MonoBehaviour
    {
        [SerializeField] private TMP_InputField m_UserName;
        [SerializeField] private TMP_InputField m_WorldName;
        [SerializeField] private RectTransform m_ProgressBar;

        public void DownloadContent()
        {
            StartCoroutine(
                ObjectLoader.DownloadWorldCO(
                    m_UserName.text,
                    m_WorldName.text,
                    UpdateProgressBar
                )
            );
        }

        public void UpdateProgressBar(float p_Progress, int p_Succes, int p_Failed)
        {
            m_ProgressBar.anchorMax = new Vector2(p_Progress, 1);
            m_ProgressBar.offsetMax = Vector2.zero;
        }
    }
}
