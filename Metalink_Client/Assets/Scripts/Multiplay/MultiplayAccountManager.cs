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

        private string _playFabPlayerIdCache;

        public void SignUp(string emailAdress, string password, string username, UnityAction<bool, string> callback = null)
        {
            var request = new RegisterPlayFabUserRequest { Email = emailAdress, Password = password, Username = username };
            PlayFabClientAPI.RegisterPlayFabUser(request, val => OnSingUpSucces(val, callback), val => OnPlayFabError(val, callback));
        }

        public void LogIn(string emailAdress, string password, UnityAction<bool, string> callback = null)
        {
            var request = new LoginWithEmailAddressRequest { Email = emailAdress, Password = password };
            PlayFabClientAPI.LoginWithEmailAddress(request, val => OnLogInSucces(val, callback), val => OnPlayFabError(val, callback));
        }

        public void OnSingUpSucces(RegisterPlayFabUserResult registerPlayFabUserResult, UnityAction<bool, string> successCallback)
        {
            successCallback?.Invoke(true, "SignUp Succes");
            Debug.Log("회원가입 성공");
        }

        public void OnLogInSucces(LoginResult loginResult, UnityAction<bool, string> successCallback)
        {
            successCallback?.Invoke(true, "LogIn Succes");
            RequestPhotonToken(loginResult);
            Debug.Log("로그인 성공");

        }

        public void OnPlayFabError(PlayFabError playFabError, UnityAction<bool, string> failedCallback = null)
        {
            failedCallback?.Invoke(false, playFabError.ErrorMessage);
            Debug.Log(playFabError.ErrorMessage);
        }

        private void RequestPhotonToken(LoginResult obj)
        {
            Debug.Log("PlayFab authenticated. Requesting photon token...");
            _playFabPlayerIdCache = obj.PlayFabId;

            PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest() {
                PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppID
            }, AuthenticateWithPhoton, val => OnPlayFabError(val));
        }

        private void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult obj)
        {
            Debug.Log("Photon token acquired: " + obj.PhotonCustomAuthenticationToken + "  Authentication complete.");

            var customAuth = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };
            customAuth.AddAuthParameter("username", _playFabPlayerIdCache);
            customAuth.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);

            PhotonNetwork.AuthValues = customAuth;
        }
    }
}
