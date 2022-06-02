using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Metalink.UI
{
    public abstract class UI_Base : MonoBehaviour
    {
        protected CanvasGroup m_Self;
        public bool m_activeSelf;

        public virtual void Awake()
        {
            m_Self = GetComponent<CanvasGroup>();
            SetActive(m_activeSelf);
        }

        public virtual void SetActive(bool p_Enable)
        {
            m_Self.alpha = p_Enable ? 1.0f : 0.0f;
            m_Self.blocksRaycasts = p_Enable;
            m_activeSelf = p_Enable;
        }

        public abstract void Enabled();

        public abstract void Disabled();
    }
}