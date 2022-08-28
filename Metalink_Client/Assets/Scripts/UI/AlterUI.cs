using UnityEngine.Events;
using UnityEngine;
using TMPro;

namespace UI
{
    public class AlterUI : UIBase
    {
        [SerializeField] private CanvasGroup alterPanel;
        [SerializeField] private TextMeshProUGUI alterText;

        private UnityAction disableAction;

        public void Enable(string text, UnityAction disableAction = null)
        {
            CanvasGroupToggle(alterPanel, true);
            alterText.text = text;
            this.disableAction = disableAction;
        }

        public void Disable()
        {
            CanvasGroupToggle(alterPanel, false);
            disableAction?.Invoke();
            disableAction = null;
        }
    }
}