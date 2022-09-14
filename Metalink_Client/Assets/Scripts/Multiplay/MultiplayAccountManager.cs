using UnityEngine.Events;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;

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

        public void SignUp(string emailAdress, string password, string username, UnityAction<bool, string> callback = null)
        {
            var request = new RegisterPlayFabUserRequest { Email = emailAdress, Password = password, Username = username };
            PlayFabClientAPI.RegisterPlayFabUser(request, val => OnSingUpSucces(val, callback), val => OnSingUpFailuer(val, callback));
        }

        public void OnSingUpSucces(RegisterPlayFabUserResult registerPlayFabUserResult, UnityAction<bool, string> successCallback)
        {
            successCallback?.Invoke(true, "SignUp Succes");
            Debug.Log("ȸ������ ����");
        }

        public void OnSingUpFailuer(PlayFabError playFabError, UnityAction<bool, string> failedCallback)
        {
            failedCallback?.Invoke(false, "SingUp Failed, " + playFabError.ErrorMessage);
            Debug.Log("ȸ������ ����, " + playFabError.ErrorMessage);
        }

        public void LogIn(string emailAdress, string password, UnityAction<bool, string> callback = null)
        {
            var request = new LoginWithEmailAddressRequest { Email = emailAdress, Password = password };
            PlayFabClientAPI.LoginWithEmailAddress(request, val => OnLogInSucces(val, callback), val => OnLogInFailuer(val, callback));
        }

        public void OnLogInSucces(LoginResult loginResult, UnityAction<bool, string> successCallback)
        {
            successCallback?.Invoke(true, "LogIn Succes");
            Debug.Log("�α��� ����");
        }

        public void OnLogInFailuer(PlayFabError playFabError, UnityAction<bool, string> failedCallback)
        {
            failedCallback?.Invoke(false, "LogIn Failed, " );
            Debug.Log("�α��� ����, " + playFabError.ErrorMessage);
        }
    }
}
