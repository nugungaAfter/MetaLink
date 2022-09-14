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

        public void SignUp(string emailAdress, string password, string username, UnityAction<string> callback = null)
        {
            var request = new RegisterPlayFabUserRequest { Email = emailAdress, Password = password, Username = username };
            PlayFabClientAPI.RegisterPlayFabUser(request, val => OnSingUpSucces(val, callback), val => OnSingUpFailuer(val, callback));
        }

        public void OnSingUpSucces(RegisterPlayFabUserResult registerPlayFabUserResult, UnityAction<string> successCallback)
        {
            Debug.Log("ȸ������ ����");
        }

        public void OnSingUpFailuer(PlayFabError playFabError, UnityAction<string> failedCallback)
        {
            Debug.Log("ȸ������ ����");

        }

        public void LogIn(string emailAdress, string password, UnityAction<string> callback = null)
        {
            var request = new LoginWithEmailAddressRequest { Email = emailAdress, Password = password };
            PlayFabClientAPI.LoginWithEmailAddress(request, val => OnLogInSucces(val, callback), val => OnLogInFailuer(val, callback));
        }

        public void OnLogInSucces(LoginResult loginResult, UnityAction<string> successCallback)
        {
            Debug.Log("�α��� ����");
        }

        public void OnLogInFailuer(PlayFabError playFabError, UnityAction<string> failedCallback)
        {
            Debug.Log("�α��� ����");

        }
    }
}
