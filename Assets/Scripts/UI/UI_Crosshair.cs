using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Metalink.UI
{
    public class UI_Crosshair : UI_Base
    {
        [SerializeField] private Image m_Image;

        public override void Enabled()
        {
            m_Image.enabled = true;
        }

        public override void Disabled()
        {
            m_Image.enabled = false;
        }
    }
}