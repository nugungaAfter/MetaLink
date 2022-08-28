using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Matalink.Keybord
{

    public class Keyboard : MonoBehaviour
    {
        public InputField inputField;
        public GameObject normalButtons;
        public GameObject capsButtons;
        private bool caps;

        // Start is called before the first frame update
        void Start()
        {
            caps = false;
        }

        public void InsertChar(string c)
        {
            inputField.text += c;
        }

        public void DeleteChar()
        {
            if (inputField.text.Length > 0)
            {
                inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
            }
        }

        public void InserSpace()
        {
            inputField.text += " ";
        }

        public void CapsPressed()
        {
            if (!caps)
            {
                normalButtons.SetActive(false);
                capsButtons.SetActive(true);
                caps = true;
            }
            else
            {
                capsButtons.SetActive(false);
                normalButtons.SetActive(true);
                caps = false;
            }
        }
    }
}
