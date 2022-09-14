using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine;

namespace Metalink.Multiplay
{
    public class MultiplayConnectionManager : MonoBehaviour, IPunCallbacks
    {
        void Awake()
        {
            MultiplayAccountManager.Instance.logInSuccessCallback += OnLoginSuccess;
        }

        public void OnLoginSuccess()
        {
            PhotonNetwork.ConnectUsingSettings(Application.version);
        }

        public void OnConnectedToPhoton()
        {
            Debug.Log("포톤에 연결됨");
        }

        public void OnLeftRoom()
        {
            Debug.Log("룸에서 떠남");
        }

        public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
        {
            Debug.Log("마스터 클라이언트 변경됨: " + newMasterClient.NickName);
        }

        public void OnPhotonCreateRoomFailed(object[] codeAndMsg)
        {
            throw new System.NotImplementedException();
        }

        public void OnPhotonJoinRoomFailed(object[] codeAndMsg)
        {
            throw new System.NotImplementedException();
        }

        public void OnCreatedRoom()
        {
            throw new System.NotImplementedException();
        }

        public void OnJoinedLobby()
        {
            throw new System.NotImplementedException();
        }

        public void OnLeftLobby()
        {
            throw new System.NotImplementedException();
        }

        public void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            throw new System.NotImplementedException();
        }

        public void OnConnectionFail(DisconnectCause cause)
        {
            throw new System.NotImplementedException();
        }

        public void OnDisconnectedFromPhoton()
        {
            throw new System.NotImplementedException();
        }

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }

        public void OnReceivedRoomListUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void OnJoinedRoom()
        {
            throw new System.NotImplementedException();
        }

        public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            throw new System.NotImplementedException();
        }

        public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            throw new System.NotImplementedException();
        }

        public void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            throw new System.NotImplementedException();
        }

        public void OnConnectedToMaster()
        {
            throw new System.NotImplementedException();
        }

        public void OnPhotonMaxCccuReached()
        {
            throw new System.NotImplementedException();
        }

        public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
        {
            throw new System.NotImplementedException();
        }

        public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
        {
            throw new System.NotImplementedException();
        }

        public void OnUpdatedFriendList()
        {
            throw new System.NotImplementedException();
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
            throw new System.NotImplementedException();
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
            throw new System.NotImplementedException();
        }

        public void OnWebRpcResponse(OperationResponse response)
        {
            throw new System.NotImplementedException();
        }

        public void OnOwnershipRequest(object[] viewAndPlayer)
        {
            throw new System.NotImplementedException();
        }

        public void OnLobbyStatisticsUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void OnPhotonPlayerActivityChanged(PhotonPlayer otherPlayer)
        {
            throw new System.NotImplementedException();
        }

        public void OnOwnershipTransfered(object[] viewAndPlayers)
        {
            throw new System.NotImplementedException();
        }
    }
}
