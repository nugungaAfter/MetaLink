using UnityEngine.UI;
using UnityEngine;

namespace Metalink.UI
{
    public class UI_Microphone : UI_Base
    {
        [SerializeField] private Image m_Image;

        private Sprite m_EnabledSprite;
        private Sprite m_DisabledSprite;

        public override void Enabled() => m_Image.sprite = m_EnabledSprite;

        public override void Disabled() => m_Image.sprite = m_DisabledSprite;

        void Awake()
        {
            m_EnabledSprite = Resources.Load("Icon/microphone_on") as Sprite;
            m_DisabledSprite = Resources.Load("Icon/microphone_off") as Sprite;
        }
    }

}