using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Matalink.UiInteraction
{
    public class UiInteraction : MonoBehaviour
    {
        public Canvas canvas;
        public Slider highSlider;
        public Text highText;
        public Slider lengthSlider;
        public Text lengthText;

        public GameObject keyBoard;

        public List<GameObject> panels;

        private void Start()
        {
            HighSlider();
            LengthSlider();
        }

        public void OnButtonClickUiCreat(GameObject Panel)
        {
            Panel.SetActive(true);
        }

        public void PanelSetActive(int PanelIndex)
        {
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].SetActive(i == PanelIndex);
            }
        }

        public void HighSlider()
        {
            int value = (int)(highSlider.value * 100);
            highText.text = value.ToString();
            keyBoard.transform.position = new Vector3(keyBoard.transform.position.x, highSlider.value, keyBoard.transform.position.z);
        }
        public void LengthSlider()
        {
            int value = (int)(lengthSlider.value * 100);
            lengthText.text = value.ToString();
            keyBoard.transform.position = new Vector3(canvas.transform.position.x - lengthSlider.value, keyBoard.transform.position.y, keyBoard.transform.position.z);
        }

        public void OnSliderDown()
        {
            keyBoard.SetActive(true);
        }

        public void OnSliderUp()
        {
            keyBoard.SetActive(false);
        }

        public void OnSubmit(InputField inputField)
        {
            keyBoard.SetActive(true);
            keyBoard.GetComponent<Matalink.Keybord.Keyboard>().inputField = inputField;
        }

        public void OnEndEdit()
        {
            keyBoard.SetActive(false);
        }
    }
}
