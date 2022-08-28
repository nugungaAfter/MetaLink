using UnityEngine.Events;
using UnityEngine;
using BackEnd;

namespace Multiplay
{
    public class MultiplayAccountManager : MonoBehaviour
    {
        private static MultiplayAccountManager instance;
        public static MultiplayAccountManager Instance
        {
            get
            {
                if (instance == null) {
                    GameObject gameObject = GameObject.Find("Multiplay Manager") ?? (new GameObject("Multiplay Manager"));
                    instance = gameObject.AddComponent<MultiplayAccountManager>();
                }
                return instance;
            }
        }

        public void SignUp(string emailAdress, string password, UnityAction<string> successCallback = null, UnityAction<string, string> failedCallback = null)
        {
            if (!Backend.IsInitialized)
                return;

            successCallback += val => Debug.Log("회원가입 성공");
            failedCallback += (msg, code) => Debug.Log("회원가입 실패");            

            var bro = Backend.BMember.CustomSignUp(emailAdress, password);
            if (bro.IsSuccess()) {
                Backend.BMember.CustomLogin(emailAdress, password);
                Backend.BMember.UpdateCustomEmail(emailAdress);
            }

            CallbackAccount(bro, successCallback, failedCallback);
        }

        public void LogIn(string emailAdress, string password, UnityAction<string> successCallback = null, UnityAction<string, string> failedCallback = null)
        {
            if (!Backend.IsInitialized)
                return;

            successCallback += val => Debug.Log("로그인 성공");
            failedCallback += (msg, code) => Debug.Log("로그인 실패");

            var bro = Backend.BMember.CustomLogin(emailAdress, password);

            CallbackAccount(bro, successCallback, failedCallback);
        }


        public void CreateNickname(string nickname, UnityAction<string> successCallback = null, UnityAction<string, string> failedCallback = null)
        {
            if (!Backend.IsInitialized)
                return;

            successCallback += val => Debug.Log("닉네임 생성 성공");
            failedCallback += (msg, code) => Debug.Log("닉네임 생성 실패");

            nickname = nickname.Trim();
            var bro = Backend.BMember.CreateNickname(nickname);

            CallbackAccount(bro, successCallback, failedCallback);
        }
        private void CallbackAccount(BackendReturnObject bro, UnityAction<string> successCallback, UnityAction<string, string> failedCallback)
        {
            if (bro.IsSuccess()) {
                successCallback?.Invoke(bro.GetMessage());
            }
            else {
                string errorMessage = bro.GetMessage();
                string errorCode = bro.GetErrorCode();
                failedCallback?.Invoke(errorMessage, errorCode);
                Debug.LogError($"[{bro.GetStatusCode()} {errorCode}] : {errorMessage}");
            }
        }
    }
}
