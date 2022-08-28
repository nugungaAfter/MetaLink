using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Matalink.Keybord
{

    public class KeyboardButton : MonoBehaviour
    {
        Keyboard g_keyboard;
        Text g_buttontext;
    
        private void Start()
        {
            g_keyboard = GetComponentInParent<Keyboard>();
            g_buttontext = GetComponentInChildren<Text>();
            if(g_buttontext.text.Length == 1)
            {
                NameToButtonText();
                GetComponentInChildren<ButtonVR>().g_onRelease.AddListener(delegate { g_keyboard.InsertChar(g_buttontext.text); });
            }
        }
        public void NameToButtonText()
        {
            g_buttontext.text = gameObject.name;
        }

    }
}
