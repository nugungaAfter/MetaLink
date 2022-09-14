using System.Text.RegularExpressions;
using UnityEngine;
using Metalink.Multiplay;
using TMPro;

namespace UI
{
    public class AccountUI : UIBase
    {
        [SerializeField] private AccountTextSetting textSetting;
        [SerializeField] private CanvasGroup selectPanel;
        [SerializeField] private CanvasGroup basePaenl;
        [SerializeField] private CanvasGroup loginPanel;
        [SerializeField] private CanvasGroup signUpPanel;
        [SerializeField] private CanvasGroup nicknamePanel;

        [Header("Input Field")]
        [SerializeField] private TMP_InputField emailIAdressInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private TMP_InputField confirmPasswordInputField;
        [SerializeField] private TMP_InputField nicknameInputField;

        [Header("Info")]
        [SerializeField] private TextMeshProUGUI progressText;

        public string Progress
        {
            get => progressText.text;
            set => progressText.text = value; 
        }

        public void LoginPanelToggle(bool active)
        {
            CanvasGroupToggle(basePaenl, active);
            CanvasGroupToggle(loginPanel, active);
            CanvasGroupToggle(selectPanel, !active);
        }

        public void SignUpPanelToggle(bool active)
        {
            CanvasGroupToggle(basePaenl, active);
            CanvasGroupToggle(signUpPanel, active);
            CanvasGroupToggle(selectPanel, !active);
        }

        public void NicknamePanelToggle(bool active)
        {
            CanvasGroupToggle(basePaenl, false);
            CanvasGroupToggle(selectPanel, !active);
            CanvasGroupToggle(nicknamePanel, active);
        }

        public void SignUp()
        {
            if (!IsAccountInputVaild())
                return;

            if(passwordInputField.text != confirmPasswordInputField.text) {
                Progress = textSetting.account_ConfirmPasswordError;
                return;
            }

            MultiplayAccountManager.Instance.SignUp(
                emailIAdressInputField.text,
                passwordInputField.text,
                nicknameInputField.text,
                (result, val) => Progress = val
            );
        }

        public void LogIn()
        {
            if (!IsAccountInputVaild())
                return;

            MultiplayAccountManager.Instance.LogIn(
                emailIAdressInputField.text,
                passwordInputField.text,
                OnLoginSuccess
            );
        }

        public void OnLoginSuccess(bool result, string message)
        {
            Progress = message;
        }

        private bool IsAccountInputVaild()
        {
            if (IsStringNull(emailIAdressInputField.text)) {
                Progress = textSetting.account_NoneInputEmailAdress;
                return false;
            }

            if (!IsEmailAdressVaild(emailIAdressInputField.text)) {
                Progress = textSetting.account_ErrorEmailAdress;
                return false;
            }

            if (IsStringNull(passwordInputField.text)) {
                Progress = textSetting.account_NoneInputPassword;
                return false;
            }

            return true;
        }

        private bool IsStringNull(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)) {
                return true;
            }

            return false;
        }

        private bool IsEmailAdressVaild(string emailAdress)
        {
            return Regex.IsMatch(emailAdress, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
        }
    }
}