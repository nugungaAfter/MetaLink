using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private Transform defaultKeyboard;
    [SerializeField] private Transform nomalKeyboard;
    [SerializeField] private Transform upperKeyboard;
    
    private TMP_InputField inputField;
    private bool isUpper = false;

    void Awake()
    {
        SetKeyboard(defaultKeyboard);
        SetKeyboard(nomalKeyboard);
        SetKeyboard(upperKeyboard);
        SetInputField();

        gameObject.SetActive(false);
    }

    private void SetInputField()
    {
        if (inputField != null)
            return;

        foreach (var inputField in GameObject.FindObjectsOfType<TMP_InputField>()) {
            var eventTrigger = inputField.gameObject.AddComponent<EventTrigger>();

            var select = new EventTrigger.Entry() { eventID = EventTriggerType.Select };
            select.callback.AddListener(data => {
                gameObject.SetActive(true);
                this.inputField = inputField;
            });
            eventTrigger.triggers.Add(select);
        }
    }

    private void SetKeyboard(Transform keyboard)
    {
        foreach (Transform key in keyboard) {
            var button = key.GetComponentInChildren<Button>();
            var text = key.GetComponentInChildren<TextMeshProUGUI>();

            if(key.gameObject.name == "BackSpace") {
                button.onClick.AddListener(() => Backspace());
            }
            else if(key.gameObject.name == "Space") {
                button.onClick.AddListener(() => AddSpace());
            }
            else if (key.gameObject.name == "Enter") {
                button.onClick.AddListener(() => Enter());
            }
            else if (key.gameObject.name == "Shift") {
                button.onClick.AddListener(() => Shift());
            }
            else {
                button.onClick.AddListener(() => AddChar(text.text));
            }
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
        this.inputField = null;
    }

    private void AddChar(string chara)
    {
        if(inputField != null)
            inputField.text += chara;
    }

    private void AddSpace()
    {
        if (inputField != null)
            inputField.text += " ";
    }

    private void Enter()
    {
        if (inputField != null)
            inputField.text += "\n";
    }

    private void Backspace()
    {
        if (inputField == null)
            return;

        string text = inputField.text;

        if (string.IsNullOrEmpty(text))
            return;

        inputField.text = text.Substring(0, text.Length - 1);
    }

    private void Shift()
    {
        nomalKeyboard.gameObject.SetActive(!isUpper);
        upperKeyboard.gameObject.SetActive(isUpper);
        isUpper = !isUpper;
    }
}
