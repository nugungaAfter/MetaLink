using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metalink.UI
{
    [System.Serializable]
    public struct UI_Tap
    {
        public CanvasGroup m_Tap;
        public CanvasGroup m_Bar;

        public void SetActive(bool p_Enable)
        {
            m_Tap.alpha = p_Enable ? 1.0f : 0.0f;
            m_Tap.blocksRaycasts = p_Enable;

            m_Bar.alpha = p_Enable ? 1.0f : 0.5f;
        }
    }

    public class UI_Menu : UI_Base
    {
        [SerializeField] private UI_Tap[] m_Taps;
        private UI_Tap m_CurrentTap;

        public void Reset() => base.Awake();

        public override void Awake()
        {
            base.Awake();
            m_CurrentTap = m_Taps[0];
            m_CurrentTap.SetActive(true);
        }

        public override void Enabled() => SetActive(true);

        public override void Disabled() => SetActive(false);

        public void TapChange(int index)
        {
            m_CurrentTap.SetActive(false);
            m_Taps[index].SetActive(true);
            m_CurrentTap = m_Taps[index];
        }
    }
}
