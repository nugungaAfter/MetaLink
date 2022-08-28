using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using Multiplay;

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
        [SerializeField] private AlterUI alterUI;

        [Header("Input Field")]
        [SerializeField] private TMP_InputField emailIAdressInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private TMP_InputField confirmPasswordInputField;
        [SerializeField] private TMP_InputField nicknameInputField;


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
                alterUI.Enable(textSetting.account_ConfirmPasswordError);
                return;
            }

            MultiplayAccountManager.Instance.SignUp(
                emailIAdressInputField.text,
                passwordInputField.text,
                (val) => {
                    alterUI.Enable(textSetting.account_SuccesSignUp);
                    SignUpPanelToggle(false);
                    NicknamePanelToggle(true);
                },
                (msg, code) => alterUI.Enable(textSetting.GetErrorMessage(msg, code))
            );
        }

        public void LogIn()
        {
            if (!IsAccountInputVaild())
                return;

            MultiplayAccountManager.Instance.LogIn(
                emailIAdressInputField.text,
                passwordInputField.text,
                (val) => {
                    alterUI.Enable(textSetting.account_SuccesLogin);

                    if (string.IsNullOrEmpty(BackEnd.Backend.UserNickName))
                        NicknamePanelToggle(true);
                    else
                        Scene.SceneLoadManager.LoadScene("Lobby");
                },
                (msg, code) => alterUI.Enable(textSetting.GetErrorMessage(msg, code))
            );
        }

        public void CreateNickname()
        {
            if (IsStringNull(nicknameInputField.text)) {
                alterUI.Enable(textSetting.account_NoneInputNickName);
                return;
            }

            MultiplayAccountManager.Instance.CreateNickname(
                nicknameInputField.text,
                (val) => {
                    Scene.SceneLoadManager.LoadScene("Lobby");
                },
                (msg, code) => alterUI.Enable(textSetting.GetErrorMessage(msg, code))
            );
        }

        private bool IsAccountInputVaild()
        {
            if (IsStringNull(emailIAdressInputField.text)) {
                alterUI.Enable(textSetting.account_NoneInputEmailAdress);
                return false;
            }

            if (!IsEmailAdressVaild(emailIAdressInputField.text)) {
                alterUI.Enable(textSetting.GetErrorMessage("Invalid email input"));
                return false;
            }

            if (IsStringNull(passwordInputField.text)) {
                alterUI.Enable(textSetting.account_NoneInputPassword);
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